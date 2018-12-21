using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Foundation.Metadata;

namespace Jitsukawa.Extensions
{
    /// <summary>
    /// Implementa acesso ao mecanismo de binding da plataforma para o uso do MVVM.
    /// (Esta classe é uma variação da BindableBase que acompanhava alguns templates do Visual Studio 2012.)
    /// </summary>
    [WebHostHidden]
    public abstract class BindableBase : INotifyPropertyChanged
    {
        private bool modified = false;

        /// <summary>
        /// Indica se o objeto sofreu alguma alteração.
        /// </summary>
        public bool Modified
        {
            get { return modified; }
            set { modified = value; }
        }

        /// <summary>
        /// Multicast event for property change notifications.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Checks if a property already matches a desired value.  Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected bool SetProperty<T>(ref T storage, T value, [CallerMemberName] String propertyName = null)
        {
            if (object.Equals(storage, value)) return false;

            storage = value;
            this.OnPropertyChanged(propertyName);
            return modified = true;
        }
        
        /// <summary>
        /// Notifies listeners that a property value has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property used to notify listeners.  This
        /// value is optional and can be provided automatically when invoked from compilers
        /// that support <see cref="CallerMemberNameAttribute"/>.</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var eventHandler = this.PropertyChanged;
            if (eventHandler != null)
            {
                eventHandler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}