using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using Delineate.Fast.Core.Commands;
using Delineate.Fast.Core.Outputs;

namespace Delineate.Fast.Core.Nodes
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
        /// The name of the template file to use
        /// </summary>
        /// <returns>The name of the template file</returns>
        public string TemplateFileName { get; private set; }

        #endregion

        /// <summary>
        /// Adds the template to be used
        /// </summary>
        /// <param name="templateName">The name of the file to be used as the template</param>
        public FileNode AddTemplate(string templateName)
        {
            TemplateFileName = templateName;
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
                Command.Outputs.SendWarning(string.Format("File '{0}' will be overwritten", Name));
            }
            else
            {
                Command.Outputs.SendNormal(string.Format("File '{0}' will be created", Name));
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
                Command.Outputs.SendWarning(string.Format("File '{0}' will be deleted", Name));
            }
            else
            {
                Command.Outputs.SendNormal(string.Format("File '{0}' doesn't exist", Name));
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
            //TODO: Need to remove the hard coding from HERE!

            string from = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), 
                                        "plugins",
                                        "local",
                                        "dev",
                                        TemplateFileName);

            File.Copy(from, FullPath);

            Command.Outputs.SendSuccess(string.Format("File '{0}' created from '{1}'", Name, TemplateFileName));
        }

        /// <summary>
        /// Adds an empty file
        /// </summary>
        private void ApplyEmptyFile()
        {
            File.Create(FullPath);

            Command.Outputs.SendSuccess(string.Format("File '{0}' created", Name));
        }

        /// <summary>
        /// Deletes a particular file 
        /// </summary>
        private void ApplyDeleteFile()
        {
            File.Delete(FullPath);

            Command.Outputs.SendSuccess(string.Format("File '{0}' deleted", Name, TemplateFileName));
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