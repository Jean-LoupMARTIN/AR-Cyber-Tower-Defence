
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.ARFoundation;

public class GrabMan : MonoBehaviour
{
    public static GrabMan inst;

    // Ref
    bool test;
    Camera cam;


    // Init
    bool initBase = true;
    public ARRaycastManager arRaycastManager;


    // Grab
    [HideInInspector] public Tower tower;
    public RectTransform rtCancelPurchase;
    bool replace = false;

    // Events
    public UnityEvent
        activeBaseEvent, placeBaseEvent,
        grabEvent, ungrabEvent, grabActiveEvent,
        replaceEvent, unreplaceEvent;


    void Awake()
    {
        inst = this;
    }

    void Start()
    {
        Base.inst.gameObject.SetActive(false);
        test = ProjectMan.test;
        cam = ProjectMan.inst.cam;
    }


    void Update()
    {
        if (initBase)
            InitBase();


        else if (tower)
            Drag();
    }

    private void Drag()
    {
        Vector3 pos;

        if ((test && Tool.MouseHit(cam, out pos, ProjectMan.LayerMask_NAR_Ground)) ||
            (!test && Tool.ScreenCenterHitAR(ProjectMan.inst.cam, arRaycastManager, out pos)))
        {
            // rotation
            Vector3 camProj = cam.transform.position;
            camProj.y = pos.y;
            Tool.LookDir(tower, Tool.Dir(pos, camProj, false));

            // position
            tower.smoothTranslate.SetTarget(pos);

            // active
            if (!replace && !tower.gameObject.active)
            {
                tower.gameObject.SetActive(true);
                grabActiveEvent.Invoke();
            }
        }


        if (Tool.Click())
        {
            if (!replace && tower.gameObject.active && !Tool.MouseInRT(rtCancelPurchase))
                Buy();
            else if (replace)
                unreplace();
        }
    }

    private void Buy()
    {
        tower.hologram.Dissolve();
        Shop.inst.AddMoney(-tower.cost);
        tower = null;
        ungrabEvent.Invoke();
    }

    private void unreplace()
    {
        tower = null;
        unreplaceEvent.Invoke();
    }

    public void CancelPurchase()
    {
        Destroy(tower.gameObject);
        tower = null;
        ungrabEvent.Invoke();
    }

    void InitBase() {
        Vector3 pos;

        if ((test && Tool.MouseHit(cam, out pos, ProjectMan.LayerMask_NAR_Ground)) ||
            (!test && Tool.ScreenCenterHitAR(ProjectMan.inst.cam, arRaycastManager, out pos)))
        {
            // position
            Base.inst.smoothTranslate.SetTarget(pos);

            // active
            if (!Base.inst.gameObject.active)
            {
                Base.inst.gameObject.SetActive(true);
                activeBaseEvent.Invoke();
            }
        }


        if (Base.inst.gameObject.active && Tool.Click())
        {
            initBase = false;
            Base.inst.hologram.Dissolve();
            placeBaseEvent.Invoke();
        }
    }


    public void Grab(Tower tower) {
        replace = false;
        this.tower = tower;
        grabEvent.Invoke();
    }

    public void Replace() {
        replace = true;
        tower = SightTower.inst.target;
        replaceEvent.Invoke();
    }
}
