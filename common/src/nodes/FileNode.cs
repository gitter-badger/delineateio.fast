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
    public sealed class FileNode : ActionNode
    {
        #region Properties 

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

        #endregion

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
        public override bool Plan()
        {
            switch(this.Operation)
            {
                case NodeOperation.Create:
                    return PlanCreateFile();

                case NodeOperation.Delete:
                    return PlanDeleteFile();

                default:
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Creates the file as required
        /// </summary>
        public override void Apply()
        {
            switch(this.Operation)
            {
                case NodeOperation.Create:
                    if(HasTemplate)
                        ApplyFileFromTemplate();
                    else
                        ApplyEmptyFile();

                    break;

                case NodeOperation.Delete:
                    ApplyDeleteFile();
                    break;

                default:    
                    throw new ArgumentException();
            }
        }

        #region Plan Methods

        /// <summary>
        /// Plans the creation of a file
        /// </summary>
        /// <returns>Returns true if the file exists</returns>
        private bool PlanCreateFile()
        {
            string path = FullPath;

            if(File.Exists(path))
            {
                Command.Output(string.Format("File '{0}' will be overwritten", Name), 
                                ConsoleColor.Red, 
                                indent: Indent);
            }
            else
            {
                Command.Output(string.Format("File '{0}' will be created", Name), 
                                ConsoleColor.White, 
                                indent: Indent);
            }

            return File.Exists(path);
        }

        /// <summary>
        /// Plans the deletion of a file
        /// </summary>
        /// <returns>Returns true if the file exists</returns>
        private bool PlanDeleteFile()
        {
            string path = FullPath;

            if(File.Exists(path))
            {
                Command.Output(string.Format("File '{0}' will be deleted", Name), 
                                ConsoleColor.Red, 
                                indent: Indent);
            }
            else
            {
                Command.Output(string.Format("File '{0}' doesn't exist", Name), 
                                ConsoleColor.White, 
                                indent: Indent);
            }

            return File.Exists(path);
        }

        #endregion

        #region Apply Methods

        /// <summary>
        /// Adds a file from a template
        /// </summary>
        private void ApplyFileFromTemplate()
        {
            //TODO: Remove the template dir hard coding

            string from = string.Format("{0}{1}templates{1}dev{1}{2}", 
                                        Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                                        Path.DirectorySeparatorChar, 
                                        TemplateName);

            File.Copy(from, FullPath);

            Command.Output(string.Format("File '{0}' created from '{1}'", Name, TemplateName), 
                                ConsoleColor.Green, 
                                indent: Indent);
        }

        /// <summary>
        /// Adds an empty file
        /// </summary>
        private void ApplyEmptyFile()
        {
            File.Create(FullPath);

            Command.Output(string.Format("File '{0}' created", Name), 
                                ConsoleColor.Green, 
                                indent: Indent);
        }

        /// <summary>
        /// Deletes a particular file 
        /// </summary>
        private void ApplyDeleteFile()
        {
            File.Delete(FullPath);

            Command.Output(string.Format("File '{0}' deleted", Name, TemplateName), 
                                ConsoleColor.Green, 
                                indent: Indent);
        }

        #endregion

        private string FullPath
        {
            get
            {
                return string.Format("{0}{1}{2}", 
                                WorkingDirectory.FullName, 
                                Path.DirectorySeparatorChar, 
                                Name); 
            }
        }
    }
}