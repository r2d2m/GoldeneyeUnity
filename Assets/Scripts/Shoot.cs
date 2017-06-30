﻿using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour
{
    public int magazineBulletCount, totalBulletCount;
    public float speedDifference;

    public AudioClip shootBullet;
    public AudioClip reloadWeapon;
    public float volume = 0.5f;

    private AudioSource source;
    private Animation animate;

    AmmoSetter _magazineAmmo;
    AmmoSetter _totalAmmo;
    ScopeActive _scopeActive;

    MovementScript _move;

    public int magazineSize;
    public int WeaponAmmoSize;

    private float normalSpeed;

    void Start()
    {
        // Initialize ammo values
        magazineBulletCount = magazineSize;
        totalBulletCount = WeaponAmmoSize;

        // UI instances
        GameObject currentAmmoText = GameObject.Find("MagazineAmmo");
        GameObject reserveAmmoText = GameObject.Find("TotalAmmo");
        GameObject scopeImage = GameObject.Find("Scope");
        _magazineAmmo = currentAmmoText.GetComponent<AmmoSetter>();
        _totalAmmo = reserveAmmoText.GetComponent<AmmoSetter>();
        _scopeActive = scopeImage.GetComponent<ScopeActive>();

        // Player instances
        GameObject player = GameObject.Find("007");
        _move = player.GetComponent<MovementScript>();
        normalSpeed = _move.speed;

        // UI set data
        _magazineAmmo.RefreshData(magazineBulletCount);
        _totalAmmo.RefreshData(totalBulletCount);
        _scopeActive.Active(false);
    }

    void Awake()
    {
        animate = GetComponent<Animation>();
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Time.timeScale > 0 && !animate.IsPlaying("Reload"))
        {
            if (_move.speed != normalSpeed) {
                _move.ModifySpeed(speedDifference);
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _move.ModifySpeed(-speedDifference);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) && !animate.IsPlaying("Shoot") && magazineBulletCount > 0)
            {
                animate.Play("Shoot");
                source.PlayOneShot(shootBullet, volume);

                Ray ray = Camera.main.ScreenPointToRay(Vector3.forward);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.tag == "Enemy")
                    {
                        IASoldiers _iaSoldier = hit.collider.gameObject.GetComponent<IASoldiers>();
                        _iaSoldier.HitAndDecreaseHP(2f);
                    }

                }

                --magazineBulletCount;
                _magazineAmmo.RefreshData(magazineBulletCount);
            }

            else if ((Input.GetKeyDown(KeyCode.R) || (magazineBulletCount == 0 && totalBulletCount != 0)) && magazineBulletCount < magazineSize)
            {
                if (totalBulletCount > 0)
                {
                    _scopeActive.Active(false);
                    animate.Play("Reload");
                    source.PlayOneShot(reloadWeapon, volume);
                    // Reassign ammo data
                    int currentAmmo = magazineBulletCount;
                    if (totalBulletCount + currentAmmo >= magazineSize)
                    {
                        magazineBulletCount = magazineSize;
                        totalBulletCount -= (magazineSize - currentAmmo);
                    }
                    else
                    {
                        magazineBulletCount += totalBulletCount;
                        totalBulletCount = 0;
                    }
                    // Print HUD
                    _magazineAmmo.RefreshData(magazineBulletCount);
                    _totalAmmo.RefreshData(totalBulletCount);
                    _scopeActive.Active(true);
                }

                else if (!source.isPlaying)
                {
                    source.PlayOneShot(reloadWeapon, volume);
                }
            }
        }
    }

    public void AddAmmo()
    {
        totalBulletCount += magazineSize;
        _totalAmmo.RefreshData(totalBulletCount);
    }
}