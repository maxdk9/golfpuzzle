


using DG.Tweening;
using UnityEngine;

public enum Direction { DEFAULT, RIGHT, LEFT, TOP, BOTTOM }
public enum CanvasType {OVERLAY, CAMERATYPE}

public class HideBeyondScreenComponent : MonoBehaviour
{
    [SerializeField] private Direction direction;
    [SerializeField] private CanvasType canvasType;
    [SerializeField] private float timeForHiding = .3f;
    [SerializeField] private float offset = 50;
    [SerializeField] private Ease easetype=Ease.InElastic;
    
    private Vector3 startPoint;
    private RectTransform rectTransform;
    private Vector2 topRightCoord;
    private Vector2 bottomLeftCoord;

    private void Start()
    {
        rectTransform = transform as RectTransform;
        startPoint = rectTransform.localPosition;
        Camera camera = null;

        if (canvasType == CanvasType.CAMERATYPE)
            camera = Camera.main;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, new Vector2(Screen.width, Screen.height), camera, out topRightCoord);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, new Vector2(0, 0), camera, out bottomLeftCoord);
        //Hide();
    }

    public void Show()
    {
        rectTransform.DOLocalMove(startPoint, timeForHiding);
    }

    public void Hide()
    {
        switch (direction)
        {
            case Direction.LEFT:
                rectTransform.DOLocalMove(new Vector3(EndPosition(Direction.LEFT).x, rectTransform.localPosition.y, 0), timeForHiding).SetEase(easetype);
                break;
            case Direction.RIGHT:
                rectTransform.DOLocalMove(new Vector3(EndPosition(Direction.RIGHT).x, rectTransform.localPosition.y, 0), timeForHiding).SetEase(easetype);
                break;
            case Direction.TOP:
                rectTransform.DOLocalMove(new Vector3(rectTransform.localPosition.x, EndPosition(Direction.TOP).y, 0), timeForHiding).SetEase(easetype);
                break;
            case Direction.BOTTOM:
                rectTransform.DOLocalMove(new Vector3(rectTransform.localPosition.x, EndPosition(Direction.BOTTOM).y, 0), timeForHiding).SetEase(easetype);
                break;
        }
    }

    private Vector3 NegativeCompensation()
    {
        return new Vector2((-rectTransform.sizeDelta.x - offset) + rectTransform.sizeDelta.x * rectTransform.pivot.x,
                        (-rectTransform.sizeDelta.y - offset) + rectTransform.sizeDelta.y * rectTransform.pivot.y);
    }

    private Vector3 PositiveCompensation()
    {
        return new Vector2((rectTransform.sizeDelta.x * rectTransform.pivot.x) + offset,
                                (rectTransform.sizeDelta.y * rectTransform.pivot.y) + offset);
    }

    private Vector2 EndPosition(Direction direction)
    {
        switch (direction)
        {
            case Direction.LEFT:
                return ((Vector3)bottomLeftCoord + rectTransform.localPosition) + NegativeCompensation();
            case Direction.RIGHT:
                return (Vector3)topRightCoord + rectTransform.localPosition + PositiveCompensation();
            case Direction.TOP:
                return ((Vector3)topRightCoord + rectTransform.localPosition) + PositiveCompensation();
            case Direction.BOTTOM:
                return ((Vector3)bottomLeftCoord + rectTransform.localPosition) + NegativeCompensation();
        }

        return startPoint;
    }
}