using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;
using static UnityEditor.AI.NavMeshBuilder;

public class PlayerControl : MonoBehaviour
{
    private Vector2 move;
    private Animator animator;
    private Grid grid;
    [SerializeField] private GameObject UI;
    [SerializeField] private GridLayout gridLayout;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject bombPrefab;
    public float speed;
    private float height_offset = 0.5f;
    [SerializeField] private NavMeshSurface surface;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    void Update()
    {
        Move();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();    
    }

    private void Move()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);
        if(movement != Vector3.zero)
        {
            animator.SetBool("IsRunning", true);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(movement), 0.15f);
        }
        else 
            animator.SetBool("IsRunning", false);
        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    private void DropBomb(InputAction.CallbackContext context)
    {
        if (context.performed)
            SnapToGrid();
    }

    private void SnapToGrid()
    {
        Vector3Int cell_pos = gridLayout.WorldToCell(transform.position);
        var obj = Instantiate(bombPrefab, grid.GetCellCenterWorld(cell_pos) + new Vector3(0, height_offset, 0), new Quaternion());
        Invoke("RebuildNavMesh", 3.5f);
    }

    private void RebuildNavMesh()
    {   
        surface.BuildNavMesh();
    }


    public void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag ("Explosion"))
        {
            UI.GetComponent<SceneManagement>().ShowUI(false);
            Destroy(this.gameObject);
        }
        else if (other.CompareTag ("Finish"))
        {
            UI.GetComponent<SceneManagement>().ShowUI(true);
            Destroy(this.gameObject);
        }
    }

    public void OnCollisionEnter (Collision other)
    {
        if (other.collider.CompareTag ("Enemy"))
        {
            UI.GetComponent<SceneManagement>().ShowUI(false);
            Destroy(this.gameObject);
        }
    }
}
