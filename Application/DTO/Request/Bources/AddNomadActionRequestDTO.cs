
namespace Application.DTO.Request.Bources;

public class AddNomadActionRequestDTO : BaseBorceRequestDTO
{
    /// <summary>
    /// //ردیف
    /// </summary>
    public long NTran { get; set; }

    /// <summary>
    /// زمان
    /// </summary>
    public TimeOnly HEven { get; set; }

    /// <summary>
    /// حجم
    /// </summary>
    public long QTitTran { get; set; }

    /// <summary>
    /// قیمت
    /// </summary>
    public decimal PTran { get; set; }

    public bool IsDisable { get; set; }

    public int NomadDateId { get; set; }
}
