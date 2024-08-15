namespace Accelerator.Entities.Backend.Response;

/// <summary>
/// Class response transactional service
/// </summary>
/// <typeparam name="T"></typeparam>
public class Response<T> where T : class, new()
{
    /// <summary>
    /// Gets or sets a value indicating whether [transaction complete].
    /// </summary>
    /// <value>
    ///   <c>true</c> if [transaction made]; otherwise, <c>false</c>.
    /// </value>
    public bool TransactionComplete { get; set; }

    /// <summary>
    /// Gets or sets the response code.
    /// </summary>
    /// <value>
    /// The response code.
    /// </value>
    public int ResponseCode { get; set; }

    /// <summary>
    /// Gets or sets the message list.
    /// </summary>
    /// <value>
    /// The message list.
    /// </value>
    public IList<string> Message { get; set; }

    /// <summary>
    /// Gets or sets the data.
    /// </summary>
    /// <value>
    /// The data.
    /// </value>
    public IList<T> Data { get; set; }
}
