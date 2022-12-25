using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDecoratable : MonoBehaviour, IBulletDecoratable
{
    public void SpawnBulletHoles(RaycastHit hit, WeaponStats weaponStats)
    {
        GameObject bulletHoleShaderCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bulletHoleShaderCube.transform.localScale = new Vector3(weaponStats.shootMarkScale.x, weaponStats.shootMarkScale.y, weaponStats.shootMarkScale.z);
        bulletHoleShaderCube.AddComponent<DestroyWhenNotInUse>();

        Renderer bulletHoleCubeRenderer = bulletHoleShaderCube.GetComponent<Renderer>();

        Material mat = new Material(Shader.Find("Universal Render Pipeline/NiloCat Extension/Screen Space Decal/Unlit"));
        
        Texture bulletHoleTexture = weaponStats.shootMarks[Random.Range(0, weaponStats.shootMarks.Length)];
        mat.SetTexture("_MainTex", bulletHoleTexture);

        bulletHoleCubeRenderer.material = mat;

        Instantiate(bulletHoleShaderCube, hit.point, Quaternion.LookRotation(hit.normal));
    }

    public void SpawnBulletHoles(Collision collision, WeaponStats weaponStats)
    {
        GameObject bulletHoleShaderCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        bulletHoleShaderCube.transform.localScale = new Vector3(weaponStats.shootMarkScale.x, weaponStats.shootMarkScale.y, weaponStats.shootMarkScale.z);
        bulletHoleShaderCube.AddComponent<DestroyWhenNotInUse>();

        Renderer bulletHoleCubeRenderer = bulletHoleShaderCube.GetComponent<Renderer>();

        Material mat = new Material(Shader.Find("Universal Render Pipeline/NiloCat Extension/Screen Space Decal/Unlit"));
        
        Texture bulletHoleTexture = weaponStats.shootMarks[Random.Range(0, weaponStats.shootMarks.Length)];
        mat.SetTexture("_MainTex", bulletHoleTexture);

        bulletHoleCubeRenderer.material = mat;

        Instantiate(bulletHoleShaderCube, collision.contacts[0].point, Quaternion.LookRotation(collision.contacts[0].normal));
    }
}
