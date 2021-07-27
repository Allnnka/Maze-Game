using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;



public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.18f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;


    Vector3 velocity;
    bool isGrounded;

    public Inventory inventory;
    public GameObject Hand;
    public HUD Hud;
    private InventoryItemBase mCurrentItem = null;


    public int maxHealth = 100;
    public int currentHealth;

    private void Start()
    {
        inventory.ItemUsed += Inventory_ItemUsed;
        inventory.ItemRemoved += Inventory_ItemRemoved;
        currentHealth = maxHealth;
        Hud.SetMaxHealth(maxHealth);
    }
    private void SetItemActive(InventoryItemBase item, bool active)
    {
        if (item != null)
        {
            GameObject currentItem = (item as MonoBehaviour).gameObject;
            currentItem.SetActive(active);
            currentItem.transform.parent = active ? Hand.transform : null;
        }
    }
    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        InventoryItemBase item = (InventoryItemBase)e.Item;

        GameObject goItem = item.gameObject;
        goItem.SetActive(true);
        goItem.transform.parent = null;

        if (item == mCurrentItem)
            mCurrentItem = null;

    }
    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
         // If the player carries an item, un-use it (remove from player's hand)
            if (mCurrentItem != null)
            {
                SetItemActive(mCurrentItem, false);
            }

            InventoryItemBase item = (InventoryItemBase)e.Item;

            // Use item (put it to hand of the player)
            SetItemActive(item, true);

            mCurrentItem = (InventoryItemBase)e.Item;
    }

    // Update is called once per frame
    void Update()
    {
        if (mItemToPickup!=null && Input.GetKeyDown(KeyCode.E))
        {
            inventory.AddItem(mItemToPickup);
            mItemToPickup.OnPickUp();
            Hud.CloseMessagePanel();
        }
        if (mItemToPickup != null && Input.GetKeyDown(KeyCode.G))
        {
            inventory.RemoveItem(mItemToPickup);
            mItemToPickup.OnDrop();
        }


        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(1);
        }
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Hud.SetHealth(currentHealth);
    }

    private IInventoryItem mItemToPickup = null;  
    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();

        if (item != null)
        {
            mItemToPickup = item;
            Hud.OpenMessagePanel("");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();

        if (item != null)
        {
            Hud.CloseMessagePanel();
            mItemToPickup = null;
        }
    }
    void Die()
    {
        Hud.EnableDeathMenu();
    }
}
