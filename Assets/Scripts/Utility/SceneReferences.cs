using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneReferences : Singleton<SceneReferences>
{
    #region serializable variables
    [SerializeField]List<SealSpeciesData> _sealSpecies;
    #endregion

    #region local variables
    #endregion

    #region getters and setters
    public List<SealSpeciesData> SealSpecies { get { return _sealSpecies; } }
    #endregion
}
