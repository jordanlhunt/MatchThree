using System;
using JewelJam.Engine;
namespace JewelJam;
public class Jewel : GameObjectSprite
{
    #region Member Variables
    #endregion
    #region Properties
    public int Type
    {
        get;
        private set;
    }
    #endregion
    #region Constructors
    public Jewel(int type) : base("spr_single_jewel" + type)
    {
    }
    #endregion
    #region Public Methods
    #endregion
    #region Private Methods
    #endregion
}
