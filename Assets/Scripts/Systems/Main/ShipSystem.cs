using System;
using System.Collections;
using System.Collections.Generic;
using AMVC.Core;
using AMVC.Systems;
using AMVC.Views.Main.History;
using UnityEngine;
using Application = AMVC.Core.Application;

public class ShipSystem : AppSystem
{
    [SerializeField] private string shipItemName;
    private List<shipItem> _items;
    private bool _isGenerated;
    private shipItem _selectedShipItem;

    private ShipPanel _p;

    private ShipPanel _panel
    {
        get
        {
            if (_p == null) _p = GetPanel<ShipPanel>();
            return _p;
        }
    }
    protected override void ReleaseReferences()
    {
        base.ReleaseReferences();
        _items = null;
        _p = null;

    }
    public override void Initialize(AppController controller, Application app)
    {
        base.Initialize(controller, app);
        shipItem.OnSelect += OnSelectMissionItem;
    }

    private void OnSelectMissionItem(shipItem missionItem)
    {
        if(_selectedShipItem != null) _selectedShipItem.Unselect();
        _selectedShipItem = missionItem;
        _selectedShipItem.Select();
      
    }
    public void Generate()
    {
        if (!_isGenerated) Clear();

        var pool = GetSystem<PoolSystem>();
        var panel = GetPanel<ShipPanel>();

        foreach (var shipModel in application.models.ships)
        {
            var item = pool.Spawn<shipItem>(this.shipItemName);
            item.Initialize(this.application);
            item.BindData(shipModel);
            _items.Add(item);
            panel.AddItem(item);
        }

        panel.GenerateItemsComplete(_items.Count);
        _isGenerated = true;
    }

    public void Clear()
    {
        if (_items == null)
        {
            _items = new List<shipItem>();
            return;
        }

        foreach (var item in _items) item.Remove();
        _items.Clear();
    }
    
}