namespace MmorpgClassLibrary.DTOs;

public class JeKupioView {
    public int Id { get; protected set; }
    public IgracView? Igrac { get; set; }
    public OrudjeView? ShoppableOrudje { get; set; }
}