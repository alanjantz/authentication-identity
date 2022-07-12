using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Jantz.Authentication.Domain.Common
{
    public class DomainNotification
    {
        private readonly List<string> _errors;

        public DomainNotification()
        {
            _errors = new List<string>();
        }

        public bool IsValid => !_errors.Any();

        public IReadOnlyCollection<string> Errors => _errors;

        public void AddNotification(string notification)
        {
            _errors.Add(notification);
        }

        public void AddNotification(IEnumerable<string> notifications)
        {
            ValidateNotification(notifications);
            _errors.AddRange(notifications);
        }

        public void AddNotification(ValidationResult validation)
        {
            ValidateNotification(validation);
            _errors.AddRange(validation.Errors.Select(x => x.ErrorMessage));
        }

        public void ClearErrors()
        {
            _errors.Clear();
        }

        private void ValidateNotification(object notification)
        {
            if (notification is null)
                throw new ArgumentNullException(nameof(notification));
        }
    }
}
