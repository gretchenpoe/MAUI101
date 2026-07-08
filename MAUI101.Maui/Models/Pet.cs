namespace MAUI101.Maui.Models;
public class Pet
{
    public string ID { get; set; }
    public string Url { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public List<Breed> Breeds { get; set; }
}