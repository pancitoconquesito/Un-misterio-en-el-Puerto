using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPOWER 
{
    public void TryExecute(movementPJ m_movementPJ);
    public void Execute(GLOBAL_TYPE.LADO _lado);
}
