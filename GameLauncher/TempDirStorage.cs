using System;
using System.IO;
namespace GameLauncher
{
    /// <summary>
    /// Represents a temporary directory storage on file system.
    /// </summary>
    public sealed partial class TempDirStorage
         : IDisposable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TempDirStorage"/> class.
        /// </summary>
        /// <param name="path">The path to use as temp storage.</param>
        public TempDirStorage(bool deleteIfExistingContent = true, string path = null)
        {
            if (path == null)
                path = Path.Combine(System.IO.Path.GetTempPath(), $"{System.Windows.Forms.Application.ProductName}\\_temporary_directory\\");
            this.tempDir = path;
            Init(deleteIfExistingContent);
        }
        public TempDirStorage(string suffixDir, bool deleteIfExistingContent = true, string path = null)
        {
            if (path == null)
            {
                if (suffixDir.Contains(":"))
                    suffixDir = Path.GetFileName(suffixDir);
                path = $"{GetMultiThreadParentDir()}\\{suffixDir}\\";
            }
            this.tempDir = path;
            Init(deleteIfExistingContent);
        }
        private static string GetMultiThreadParentDir()
        {
            return Path.Combine(System.IO.Path.GetTempPath(), $"{System.Windows.Forms.Application.ProductName}\\_tmp_dir_");
        }
        public static void DeleteTempMultithreadDir()
        {
            try
            {
                if (Directory.Exists(GetMultiThreadParentDir()))
                    Directory.Delete(GetMultiThreadParentDir(), true);
            }
            catch
            {
            }
        }
        public void Init(bool deleteIfExistingContent)
        {
            if (deleteIfExistingContent)
                this.Clear();
            this.Create();
        }
        public string tempDir { get; private set; }
        private void Create()
        {
            try
            {
                if (!Directory.Exists(this.tempDir))
                {
                    Directory.CreateDirectory(this.tempDir);
                }
            }
            catch (IOException)
            {
            }
        }
        public void Clear()
        {
            try
            {
                if (Directory.Exists(this.tempDir))
                {
                    Directory.Delete(this.tempDir, true);
                }
            }
            catch (IOException)
            {
            }
        }
        #region IDisposable
        /// <summary>
        /// An indicator whether this object is being actively disposed or not.
        /// </summary>
        private bool disposed;
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Throws an exception if something is tried to be done with an already disposed object.
        /// </summary>
        /// <remarks>
        /// All public methods of the class must first call this.
        /// </remarks>
        public void ThrowIfDisposed()
        {
            if (this.disposed)
            {
                throw new ObjectDisposedException(this.GetType().Name);
            }
        }
        /// <summary>
        /// Releases managed resources upon dispose.
        /// </summary>
        /// <remarks>
        /// All managed resources must be released in this
        /// method, so after disposing this object no other
        /// object is being referenced by it anymore.
        /// </remarks>
        private void ReleaseManagedResources()
        {
            this.Clear();
        }
        /// <summary>
        /// Releases unmanaged resources upon dispose.
        /// </summary>
        /// <remarks>
        /// All unmanaged resources must be released in this
        /// method, so after disposing this object no other
        /// object is being referenced by it anymore.
        /// </remarks>
        private void ReleaseUnmanagedResources()
        {
        }
        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                /* Release unmanaged resources */
                this.ReleaseUnmanagedResources();
                if (disposing)
                {
                    /* Release managed resources */
                    this.ReleaseManagedResources();
                }
                /* Set indicator that this object is disposed */
                this.disposed = true;
            }
        }
        #endregion
    }
}