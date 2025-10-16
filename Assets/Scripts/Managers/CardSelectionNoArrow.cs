using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering;

public class CardSelectionNoArrow : CardSelectionBase
{
    private Vector3 originalCardPosition;
    private Quaternion originalCardRotation;
    private int originalCardSortingOrder;

    private const float cardCancelAnimationTime = 0.2f;
    private const Ease cardAnimationEase = Ease.OutBack;

    private const float CardAboutToBePlayedOffsetY = 1.5f;
    private const float CardAnimationTime = 0.4f;
    [SerializeField] private BoxCollider2D cardArea;

    private bool isCardAboutToBePlayed;

    private void Update()
    {
        if (cardDisplayManager.isMoving())
            return;

        if (isCardAboutToBePlayed)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            DetectCardSelection();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            DetectCardUnselection();
        }

        if (selectedCard != null)
        {
            UpdateSelectedCard();
        }
    }



    private void DetectCardSelection()
    {
        if (selectedCard != null)
            return;

        // �������Ƿ��ڿ��Ƶ��Ϸ����˵������
        var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        var hitInfo = Physics2D.Raycast(mousePosition, Vector3.forward,
            Mathf.Infinity, cardLayer);

        if (hitInfo.collider != null)
        {
            var card = hitInfo.collider.GetComponent<CardObject>();
            var cardTemplate = card.template;

            if (CardUtils.CardCanBePlayed(cardTemplate, playerMana) &&
                !CardUtils.CardHasTargetableEffect(cardTemplate))
            {
                selectedCard = hitInfo.collider.gameObject;
                originalCardPosition = selectedCard.transform.position;
                originalCardRotation = selectedCard.transform.rotation;
                originalCardSortingOrder = selectedCard.GetComponent<SortingGroup>().sortingOrder;
            }
        }
    }

    private void DetectCardUnselection()
    {
        if (selectedCard != null)
        {
            var sequence = DOTween.Sequence();
            sequence.AppendCallback(() =>
            {
                selectedCard.transform.DOMove(originalCardPosition, cardCancelAnimationTime).SetEase(cardAnimationEase);
                selectedCard.transform.DORotate(originalCardRotation.eulerAngles, cardCancelAnimationTime);
            });
            sequence.OnComplete(() =>
            {
                selectedCard.GetComponent<SortingGroup>().sortingOrder = originalCardSortingOrder;
                selectedCard = null;
            });
        }
    }

    private void UpdateSelectedCard()
    {
        // �����������Ѿ��ɿ�ʱ���߼�
        if (Input.GetMouseButtonUp(0))
        {
            var card = selectedCard.GetComponent<CardObject>();

            // ���ѡ�п��ƺ��������Ѿ��ɿ���ѡ�еĿ�����׼�����״̬
            if (card.State == CardObject.CardState.AboutToBePlayed)
            {
                // ���ô�״̬�������ε����Ƶ���קЧ��
                isCardAboutToBePlayed = true;

                // �ƶ��ǹ������Ƶ�Ч��ʩ������
                var sequence = DOTween.Sequence();

                sequence.Append(selectedCard.transform.DOMove(cardArea.bounds.center, CardAnimationTime)
                    .SetEase(cardAnimationEase));
                sequence.AppendInterval(CardAnimationTime + 0.1f);
                sequence.AppendCallback(() =>
                {
                    // ��ʼʩ��Ч��
                    PlaySelectedCard();
                    selectedCard = null;
                    isCardAboutToBePlayed = false;
                });

                selectedCard.transform.DORotate(Vector3.zero, CardAnimationTime);
            }
            //  ���ѡ�п��ƺ󣬷��ֲ���������ƣ��ҿ����ƶ��ľ��뻹�����Դ����÷ǹ������ƽ���
            //  Ч��ʩ�����Ķ�������ʱ���ÿ���״̬�����������û���ʼλ��
            else
            {
                card.SetState(CardObject.CardState.InHand);
                selectedCard.GetComponent<CardObject>().Reset(() => selectedCard = null);
            }
        }

        if (selectedCard != null)
        {
            var mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;
            selectedCard.transform.position = mousePosition;

            var card = selectedCard.GetComponent<CardObject>();

            // ���ǹ���������ѡ�к�ľ����Ƿ��㹻�󣬿��Ըı����ĳ���״̬
            // ����㹻��������������״̬�����������״̬
            if (mousePosition.y > originalCardPosition.y + CardAboutToBePlayedOffsetY)
            {
                card.SetState(CardObject.CardState.AboutToBePlayed);
            }
            else
            {
                card.SetState(CardObject.CardState.InHand);
            }
        }
    }

    // ���ػ����PlaySelectedCard���������ڽ����ǹ���Ч���࿨��
    protected override void PlaySelectedCard()
    {
        base.PlaySelectedCard();

        var card = selectedCard.GetComponent<CardObject>().runtimeCard;
        effectResolutionManager.ResolveCardEffects(card);

    }

    public bool HasSelectedCard()
    {
        return selectedCard != null;
    }
}
