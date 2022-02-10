using System;
using AMVC.Core;
using AMVC.Systems.Main;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Application = AMVC.Core.Application;

namespace AMVC.Views.Main.History
{
    public class ShipPanel : AppPanel
    {
        [Header("Internal References")]
        [SerializeField] private RectTransform scrollView;
        [SerializeField] private Button backBtn;
        [SerializeField] GameObject poPupPanel ;
        public int idShips;
        private int _totalItems;
        private int _index;
        private bool _canTransition;
        private ShipSystem _system;

        protected override void ReleaseReferences()
        {
            base.ReleaseReferences();
            scrollView = null;
            backBtn = null;
            _system = null;
        
        }

        public override void Initialize(AppView view, Application app)
        {
            base.Initialize(view, app);
            
            backBtn.onClick.AddListener(BackToMainApp);
            
        }

        public override void OpenPanel(Action callBack = null)
        {
            _canTransition = true;
            _index = 0;
            base.OpenPanel(callBack);
        }

        private void BackToMainApp()
        {
            ClosePanel(() =>
            {
                GetPanel<MenuPanel>().OpenPanel();
            });
        }

        public void AddItem(shipItem item)
        {
            item.transform.SetParent(this.scrollView);
        }

        public void GenerateItemsComplete(int total)
        {
            _totalItems = total;
        }
        
        
        
    }
}
