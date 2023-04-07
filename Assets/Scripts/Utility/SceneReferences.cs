using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReferences : Singleton<SceneReferences>
{
    #region serializable variables
    [SerializeField]List<SealSpecies> _sealSpecies;
    #endregion

    #region local variables
    #endregion

    #region getters and setters
    public List<SealSpecies> SealSpecies { get { return _sealSpecies; } }
    #endregion
}
