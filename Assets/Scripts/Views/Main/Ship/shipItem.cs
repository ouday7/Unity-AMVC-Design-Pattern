using System;
using System.Collections;
using AMVC.Core;
using AMVC.Models;
using AMVC.Systems;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Application = AMVC.Core.Application;

public class shipItem : BaseMonoBehaviour, IPoolItem
{
    public static event Action<shipItem> OnSelect;
    [SerializeField] private GameObject prefab;
    [SerializeField] Image img;
    [SerializeField] Sprite notFoundSprite;
    [SerializeField] private Button detailBtn;
    [SerializeField] private GameObject detailsPanel;
    [SerializeField] private GameObject shipItemPanel;
    [SerializeField] private string spriteLink;
    [SerializeField] private Text shipNameText;
    [SerializeField] private Text shipId;
    [SerializeField] private Text shipModelText;
    [SerializeField] private Text shipTypeText;
    [SerializeField] private Text activeText;
    [SerializeField] private Text imoText;
    [SerializeField] private Text msiText;
    [SerializeField] private Text absText;
    [SerializeField] private Text weightLbsText;
    [SerializeField] private Text weightKgText;
    [SerializeField] private Text yearBuiltText;
    [SerializeField] private Button linkBtn;
    [SerializeField] private string link;
    [SerializeField] private Text homePortText;

    private Application _application;
    private bool _isInitialized;
    
    protected override void ReleaseReferences()
    {
        spriteLink = null;
        shipNameText = null;
        shipId = null;
        shipModelText = null;
        shipTypeText = null;
        activeText = null;
        imoText = null;
        msiText = null;
        absText = null;
        weightLbsText = null;
        weightKgText = null;
        yearBuiltText = null;
        linkBtn = null;
        homePortText = null;
        _application = null;
    }
    public void Initialize(Application app)
    {
        if (_isInitialized) return;
        _isInitialized = true;
        _application = app;

        detailBtn.onClick.AddListener(() => { OnSelect?.Invoke(this); });
    }
    public void BindData(ShipModel model)
    {
        spriteLink = model.image;
        this.shipNameText.text = model.ship_name;
        this.shipId.text = model.ship_id;
        this.shipModelText.text = model.ship_model;
        this.shipTypeText.text = model.ship_type;
        this.activeText.text = model.active;
        this.imoText.text = model.imo;
        this.msiText.text = model.mmsi;
        this.absText.text = model.abs.ToString();
        this.weightLbsText.text = model.weight_lbs.ToString();
        this.weightKgText.text = model.weight_kg.ToString();
        this.yearBuiltText.text = model.year_built;
        this.link = model.url;
        this.linkBtn.onClick.AddListener(OpenLink);
        this.homePortText.text = model.home_port;
        Download(spriteLink);
    }
    public void Select()
    {
        detailBtn.interactable = false;
        prefab.transform.DORotate(new Vector3(0, 90, 0), 0.30f).OnComplete(() =>
        {
            prefab.transform.DORotate(new Vector3(0, 0, 0), 0.30f).OnComplete(() =>
            {
                detailsPanel.gameObject.SetActive(true);
                shipItemPanel.gameObject.SetActive(false);
            });
        });
    }
    public void Unselect()
    {
        detailBtn.interactable = true;
        detailsPanel.gameObject.SetActive(false);
        shipItemPanel.gameObject.SetActive(true);
    }
    private void Download(string url)
    {
        StartCoroutine(LoadFromWeb(url));
    }
    IEnumerator LoadFromWeb(string url)
    {
        UnityWebRequest wr = new UnityWebRequest(url);
        DownloadHandlerTexture texDl = new DownloadHandlerTexture(true);
        wr.downloadHandler = texDl;
        yield return wr.Send();
        if (!wr.isError)
        {
            Texture2D t = texDl.texture;
            Sprite s = Sprite.Create(t, new Rect(0, 0, t.width, t.height), Vector2.zero, 1f);
            img.sprite = s;
        }
        else
        {
            Debug.Log("404 image");
            img.sprite = notFoundSprite;
        }
    }
    public void Remove()
    {
        _application.GetSystem<PoolSystem>().Despawn(this.transform);
    }
    private void OpenLink()
    {
        UnityEngine.Application.OpenURL(link);
    }
}