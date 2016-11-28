// Copyright (c) 2013-2016 Cemalettin Dervis, MIT License.
// https://github.com/cemdervis/SharpConfig

using System;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace SharpConfig
{
    internal static class ConfigurationWriter
    {
        // We need this, as we never want to close the stream the user has given us.
        // But we also want to call the specified writer's Dispose() method.
        // We wouldn't need this if we were targeting .NET 4+, because BinaryWriter
        // gives us the option to leave the stream open after Dispose(), but not
        // on .NET lower than 4.0.
        // To circumvent this, we just define our own writer that does not close
        // the underlying stream in Dispose().
        private class NonClosingBinaryWriter : BinaryWriter
        {
            public NonClosingBinaryWriter(Stream stream)
                : base(stream)
            { }

            protected override void Dispose(bool disposing)
            { }
        }

        public static void WriteToStreamTextual(Configuration cfg, Stream stream, Encoding encoding)
        {
            Debug.Assert(cfg != null);

            if (stream == null)
                throw new ArgumentNullException("stream");

            if (encoding == null)
                encoding = new UTF8Encoding();

            var sb = new StringBuilder();

            // Write all sections.
            bool isFirstSection = true;

            foreach (var section in cfg)
            {
                if (!isFirstSection)
                    sb.AppendLine();

                // Leave some space between this section and the element that is above,
                // if this section has pre-comments and isn't the first section in the configuration.
                if (!isFirstSection && section.mPreComments != null && section.mPreComments.Count > 0)
                    sb.AppendLine();

                sb.AppendLine(section.ToString(true));

                // Write all settings.
                foreach (var setting in section)
                {
                    // Leave some space between this setting and the element that is above,
                    // if this element has pre-comments.
                    if (setting.mPreComments != null && setting.mPreComments.Count > 0)
                        sb.AppendLine();

                    sb.AppendLine(setting.ToString(true));
                }

                isFirstSection = false;
            }

            string str = sb.ToString();

            // Encode & write the string.
            var byteBuffer = new byte[encoding.GetByteCount(str)];
            int byteCount = encoding.GetBytes(str, 0, str.Length, byteBuffer, 0);

            stream.Write(byteBuffer, 0, byteCount);
            stream.Flush();
        }

        public static void WriteToStreamBinary(Configuration cfg, Stream stream, BinaryWriter writer)
        {
            Debug.Assert(cfg != null);

            if (stream == null)
                throw new ArgumentNullException("stream");

            if (writer == null)
                writer = new NonClosingBinaryWriter(stream);

            writer.Write(cfg.SectionCount);

            foreach (var section in cfg)
            {
                writer.Write(section.Name);
                writer.Write(section.SettingCount);

                WriteCommentsBinary(writer, section);

                // Write the section's settings.
                foreach (var setting in section)
                {
                    writer.Write(setting.Name);
                    writer.Write(setting.StringValue);

                    WriteCommentsBinary(writer, setting);
                }
            }
            
            writer.Close();
        }

        private static void WriteCommentsBinary(BinaryWriter writer, ConfigurationElement element)
        {
            // Write the comment.
            var commentNullable = element.Comment;

            writer.Write(commentNullable.HasValue);
            if (commentNullable.HasValue)
            {
                var comment = commentNullable.Value;
                writer.Write(comment.Symbol);
                writer.Write(comment.Value);
            }

            // Write the pre-comments.
            // Note: do not access the PreComments property of element,
            // as it will lazily create a new List of pre-comments.
            // Access the private field instead.
            var preComments = element.mPreComments;
            bool hasPreComments = (preComments != null && preComments.Count > 0);

            writer.Write(hasPreComments ? preComments.Count : 0);

            if (hasPreComments)
            {
                foreach (var preComment in preComments)
                {
                    writer.Write(preComment.Symbol);
                    writer.Write(preComment.Value);
                }
            }
        }

    }
}