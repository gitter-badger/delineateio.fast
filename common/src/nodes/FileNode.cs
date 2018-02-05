using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;

namespace Delineate.Fast.Nodes
{
    /// <summary>
    /// Represents the root node in the command that will
    /// be used during the plan and Apply command methods
    /// </summary>
    public sealed class FileNode : Node
    {
        /// <summary>
        /// Indicates that a template
        /// </summary>
        /// <returns>Returns true if a template is set</returns>
        public bool HasTemplate { get; private set; }

        /// <summary>
        /// The name of the template to use
        /// </summary>
        /// <returns>The name of the template</returns>
        public string TemplateName { get; private set; }

        /// <summary>
        /// Adds the template to be used
        /// </summary>
        /// <param name="templateName">The name of the file to be used as the template</param>
        public FileNode AddTemplate(string templateName)
        {
            TemplateName = templateName;
            HasTemplate = true;
            return this;
        } 

        /// <summary>
        /// Checks to see if the file already exists
        /// </summary>
        /// <param name="warnings">Collection of current warnings</param>
        public override void Plan(List<string> warnings)
        {
            if(File.Exists(Name))
                warnings.Add(Name);
        }

        /// <summary>
        /// Creates the file as required
        /// </summary>
        /// <param name="warnings"></param>
        public override void Apply()
        {
            switch(this.Operation)
            {
                case NodeOperation.Create:
                    if(HasTemplate)
                        AddFileFromTemplate();
                    else
                         AddEmptyFile();

                    break;

                case NodeOperation.Delete:
                    DeleteFile();
                    break;

                default:    
                    throw new ArgumentException();
            }
        }

        private void AddFileFromTemplate()
        {
            string from = string.Format("{0}{1}templates{1}standard{1}{2}", 
                                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                                        Path.DirectorySeparatorChar, 
                                        TemplateName);

            string to = string.Format("{0}{1}{2}", 
                                        WorkingDirectory.FullName, 
                                        Path.DirectorySeparatorChar, 
                                        Name);

            File.Copy(from, to);
        }

        /// <summary>
        /// Adds an empty file
        /// </summary>
        private void AddEmptyFile()
        {
            string path = string.Format("{0}{1}{2}", 
                                        WorkingDirectory.FullName, 
                                        Path.DirectorySeparatorChar, 
                                        Name);

            File.Create(path);
        }

        private void DeleteFile()
        {
                            
            string path = string.Format("{0}{1}{2}", 
                                WorkingDirectory.FullName, 
                                Path.DirectorySeparatorChar, 
                                Name);
            
            File.Delete(path);
        }
    }
}