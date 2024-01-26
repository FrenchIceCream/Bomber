using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject groundPrefab;
    public LayerMask layerMask;
    public LayerMask layerMaskDestructable;
    public AudioClip clip;
    private IEnumerator CreateExplosions(Vector3 direction) 
    {
        for (int i = 1; i < 3; i++) 
        { 
            RaycastHit hit; 
            Physics.Raycast(transform.position, direction, out hit, i, layerMask); 
    
            RaycastHit hit_destructable; 
            Physics.Raycast(transform.position, direction, out hit_destructable, i, layerMaskDestructable); 

            if (hit_destructable.collider)
            {
                var pos = transform.position + (i * direction);
                Destroy(hit_destructable.collider.gameObject);
                Instantiate(groundPrefab, new Vector3(pos.x, 0, pos.z), explosionPrefab.transform.rotation);
                Instantiate(explosionPrefab, pos, explosionPrefab.transform.rotation);
                break;
            }
            else if (!hit.collider) 
            { 
                Instantiate(explosionPrefab, transform.position + (i * direction), explosionPrefab.transform.rotation); 
            }
            else 
                break; 

            yield return new WaitForSeconds(0.05f); 
        }
    }  

    void Awake()
    {
        Invoke("Explode", 2f);
        GetComponent<BoxCollider>().isTrigger  = true;
    }

    private void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(clip, transform.position);
        StartCoroutine(CreateExplosions(Vector3.forward));
        StartCoroutine(CreateExplosions(Vector3.right));
        StartCoroutine(CreateExplosions(Vector3.back));
        StartCoroutine(CreateExplosions(Vector3.left));  

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled  = false;
        Destroy(gameObject, 0.3f);
    }

    private void OnTriggerExit(Collider other)
    {
        GetComponent<BoxCollider>().isTrigger  = false;
    }
}
