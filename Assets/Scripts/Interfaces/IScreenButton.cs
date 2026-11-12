namespace Interfaces
{
    public interface IScreenButton
    {
        /// <summary>
        /// Called whenever the IScreenButton is clicked on by the mouse
        /// </summary>
        public void Clicked();
        
        /// <summary>
        /// Called whenever the mouse lets go of IScreenButton
        /// </summary>
        public void ReleaseClick();
    }
}