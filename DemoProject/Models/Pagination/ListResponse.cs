namespace DemoProject.Models.Pagination;

public class ListResponse<T>
{
    public ListResponse(List<T>? data, int count)
    {
        Data = data;
        RecordsTotal = count;
        RecordsFiltered = count;
    }

    public List<T>? Data { get; set; }
    public int RecordsTotal { get; set; }
    public int RecordsFiltered { get; set; }
}