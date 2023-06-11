internal class MovingData
{
    public int x;
    public int y;
    public TileEnvironmentDeterminer tile;   

    public MovingData(int x, int y, TileEnvironmentDeterminer tile)
    {
        this.x = x;
        this.y = y;
        this.tile = tile;
    }
}