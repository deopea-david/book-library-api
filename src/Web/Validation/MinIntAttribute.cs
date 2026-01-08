using System.ComponentModel.DataAnnotations;
using System.Numerics;

namespace BookLibraryAPI.Web.Validation;

public class MinIntAttribute(int _min) : ValidationAttribute
{
  protected override ValidationResult IsValid(object value, ValidationContext validationContext)
  {
    if (value == null)
      return ValidationResult.Success;

    int? parsedVal = null;
    if (value is string strVal)
    {
      if (string.IsNullOrWhiteSpace(strVal))
        return ValidationResult.Success;

      if (int.TryParse(strVal, out int _intVal))
        parsedVal = _intVal;
      else
        return new ValidationResult($"Must be an integer of at least {_min}");
    }
    else if (value is int intVal)
    {
      parsedVal = intVal;
    }

    if (parsedVal == null)
      return new ValidationResult("Must be an integer");

    if (parsedVal < _min)
      return new ValidationResult($"Must be at least {_min}");

    return ValidationResult.Success;
  }
}
