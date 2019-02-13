using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace MyCryptoMonitor.Objects
{
    public abstract class NotifyPropertyChangedBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        #region Public Events

        public virtual event PropertyChangedEventHandler PropertyChanged;

        public virtual event PropertyChangingEventHandler PropertyChanging;

        #endregion Public Events

        #region Protected Properties

        protected virtual TaskScheduler TaskScheduler { get; set; }

        #endregion Protected Properties

        #region Public Methods

        public string GetName<TProperty>(Expression<Func<TProperty>> property)
        {
            var lambda = (LambdaExpression)property;

            MemberExpression memberExpression = lambda.Body is UnaryExpression unaryExpression ? (MemberExpression)unaryExpression.Operand : (MemberExpression)lambda.Body;

            return memberExpression.Member.Name;
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void FirePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                if (TaskScheduler != null)
                    Task.Factory.StartNew(() => PropertyChanged(this, new PropertyChangedEventArgs(propertyName)), CancellationToken.None, TaskCreationOptions.None, TaskScheduler);
                else
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected virtual void FirePropertyChanged<TProperty>(Expression<Func<TProperty>> property)
        {
            if (PropertyChanged != null)
                FirePropertyChanged(GetName(property));
        }

        protected virtual void FirePropertyChanging([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanging != null)
            {
                if (TaskScheduler != null)
                    Task.Factory.StartNew(() => PropertyChanging(this, new PropertyChangingEventArgs(propertyName)), CancellationToken.None, TaskCreationOptions.None, TaskScheduler);
                else
                    PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        protected virtual void FirePropertyChanging<TProperty>(Expression<Func<TProperty>> property)
        {
            if (PropertyChanging != null)
                FirePropertyChanging(GetName(property));
        }

        protected virtual bool SetValue<TValue>(ref TValue oldValue, TValue newValue, Action<TValue> afterValueSetDelegate = null, [CallerMemberName] string propertyName = null)
        {
            return SetValue(ref oldValue, ref newValue, afterValueSetDelegate, propertyName);
        }

        protected virtual bool SetValue<TProperty, TValue>(Expression<Func<TProperty>> property, ref TValue oldValue, TValue newValue, Action<TValue> afterValueSetDelegate = null)
        {
            return SetValue(property, ref oldValue, ref newValue, afterValueSetDelegate);
        }

        protected virtual bool SetValue<TProperty, TValue>(Expression<Func<TProperty>> property, ref TValue oldValue, ref TValue newValue, Action<TValue> afterValueSetDelegate = null)
        {
            if (EqualityComparer<TValue>.Default.Equals(oldValue, newValue))
                return false;

            return SetValue(ref oldValue, ref newValue, afterValueSetDelegate, GetName(property));
        }

        protected virtual bool SetValue<TValue>(ref TValue oldValue, ref TValue newValue, Action<TValue> afterValueSetDelegate = null, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<TValue>.Default.Equals(oldValue, newValue))
                return false;

            FirePropertyChanging(propertyName);
            oldValue = newValue;
            afterValueSetDelegate?.Invoke(newValue);
            FirePropertyChanged(propertyName);

            return true;
        }

        #endregion Protected Methods
    }
}