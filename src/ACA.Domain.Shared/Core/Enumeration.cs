using System.Reflection;

namespace ACA.Domain.Shared.Core;

/// <summary>
///   Base class for creating strongly-typed enumerations in C#.
/// </summary>
/// <remarks>
///   Author: Akbar Ahmadi Saray
/// </remarks>
public abstract class Enumeration : IComparable<Enumeration>
{
  /// <summary>
  ///   Constructor to initialize an enumeration member with an ID and name.
  /// </summary>
  /// <param name="id">The ID of the enumeration member.</param>
  /// <param name="name">The name of the enumeration member.</param>
  protected Enumeration(int id, string name)
  {
    (Id, Name) = (id, name);
  }

  /// <summary>
  ///   The name of the enumeration member.
  /// </summary>
  public string Name { get; }

  /// <summary>
  ///   The ID of the enumeration member.
  /// </summary>
  public int Id { get; }

  /// <summary>
  ///   Compares this instance with another enumeration member by their IDs.
  /// </summary>
  /// <param name="other">The other enumeration member to compare with.</param>
  /// <returns>An integer representing the comparison result.</returns>
  public int CompareTo(Enumeration? other)
  {
    return Id.CompareTo(other?.Id);
  }

  /// <summary>
  ///   Returns the name of the enumeration member.
  /// </summary>
  /// <returns>The name of the enumeration member.</returns>
  public override string ToString()
  {
    return Name;
  }

  /// <summary>
  ///   Generates a hash code for the enumeration member.
  /// </summary>
  /// <returns>A hash code for the enumeration member.</returns>
  public override int GetHashCode()
  {
    return HashCode.Combine(Id);
  }

  /// <summary>
  ///   NovinChecks if this enumeration member is equal to another object.
  /// </summary>
  /// <param name="obj">The object to compare with.</param>
  /// <returns>True if the objects are equal, otherwise false.</returns>
  public override bool Equals(object? obj)
  {
    if (obj is not Enumeration otherValue)
    {
      return false;
    }

    var typeMatches = GetType().Equals(obj.GetType());
    var valueMatches = Id.Equals(otherValue.Id);

    return typeMatches && valueMatches;
  }

  /// <summary>
  ///   Retrieves an enumeration member by its ID.
  /// </summary>
  /// <typeparam name="T">The type of enumeration.</typeparam>
  /// <param name="id">The ID of the enumeration member to retrieve.</param>
  /// <returns>The enumeration member with the specified ID.</returns>
  public static T FromId<T>(int id) where T : Enumeration
  {
    var matchingItem = Parse<T, int>(id, "Id", item => item.Id == id);
    return matchingItem;
  }

  /// <summary>
  ///   Retrieves an enumeration member by its name.
  /// </summary>
  /// <typeparam name="T">The type of enumeration.</typeparam>
  /// <param name="name">The name of the enumeration member to retrieve.</param>
  /// <returns>The enumeration member with the specified name.</returns>
  public static T FromName<T>(string name) where T : Enumeration
  {
    var matchingItem = Parse<T, string>(name, "Name", item => item.Name == name);
    return matchingItem;
  }

  /// <summary>
  ///   Retrieves the ID of an enumeration member based on its name.
  /// </summary>
  /// <typeparam name="T">The type of enumeration.</typeparam>
  /// <param name="name">The name of the enumeration member.</param>
  /// <returns>The ID of the enumeration member with the specified name.</returns>
  public static int GetIdFromName<T>(string name) where T : Enumeration
  {
    var item = GetAll<T>().FirstOrDefault(e => e.Name == name);
    if (item == null)
    {
      throw new KeyNotFoundException($"Enumeration member with name '{name}' not found in {typeof(T).Name}.");
    }

    return item.Id;
  }

  /// <summary>
  ///   Retrieves the name of an enumeration member based on its ID.
  /// </summary>
  /// <typeparam name="T">The type of enumeration.</typeparam>
  /// <param name="id">The ID of the enumeration member.</param>
  /// <returns>The name of the enumeration member with the specified ID.</returns>
  public static string GetNameFromId<T>(int id) where T : Enumeration
  {
    var item = GetAll<T>().FirstOrDefault(e => e.Id == id);
    if (item == null)
    {
      throw new KeyNotFoundException($"Enumeration member with ID '{id}' not found in {typeof(T).Name}.");
    }

    return item.Name;
  }

  /// <summary>
  ///   NovinChecks if the given ID corresponds to a defined enumeration member.
  /// </summary>
  /// <typeparam name="T">The type of enumeration.</typeparam>
  /// <param name="id">The ID to NovinCheck.</param>
  /// <returns>True if the ID corresponds to a defined enumeration member, otherwise false.</returns>
  public static bool IsDefined<T>(int id) where T : Enumeration
  {
    return GetAll<T>().Any(e => e.Id == id);
  }

  /// <summary>
  ///   NovinChecks if the given name corresponds to a defined enumeration member.
  /// </summary>
  /// <typeparam name="T">The type of enumeration.</typeparam>
  /// <param name="name">The name to NovinCheck.</param>
  /// <returns>True if the name corresponds to a defined enumeration member, otherwise false.</returns>
  public static bool IsDefined<T>(string name) where T : Enumeration
  {
    return GetAll<T>().Any(e => e.Name == name);
  }

  /// <summary>
  ///   Retrieves all enumeration members of the specified type.
  /// </summary>
  /// <typeparam name="T">The type of enumeration.</typeparam>
  /// <returns>A collection containing all enumeration members of the specified type.</returns>
  public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
    typeof(T).GetFields(BindingFlags.Public |
                        BindingFlags.Static |
                        BindingFlags.DeclaredOnly)
      .Select(f => f.GetValue(null))
      .Cast<T>();

  /// <summary>
  ///   Parses an enumeration member based on a given value and predicate.
  /// </summary>
  /// <typeparam name="T">The type of enumeration.</typeparam>
  /// <typeparam name="TKey">The type of value to parse.</typeparam>
  /// <param name="value">The value to parse.</param>
  /// <param name="propertyName">The name of the property being parsed (ID or Name).</param>
  /// <param name="predicate">The predicate to match enumeration members.</param>
  /// <returns>The parsed enumeration member.</returns>
  private static T Parse<T, TKey>(TKey value, string propertyName, Func<T, bool> predicate)
    where T : Enumeration
  {
    var matchingItem = GetAll<T>().FirstOrDefault(predicate);
    if (matchingItem == null)
    {
      throw new InvalidOperationException($"'{value}' is not a valid {typeof(T).Name} {propertyName}.");
    }

    return matchingItem;
  }
}
