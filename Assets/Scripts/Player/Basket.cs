using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Basket : MonoBehaviour
{
    Vector3 inputPosition;
    bool touched;

    // Переменная для определения доли экрана для управления корзиной
    [Range(0.1f, 1.0f)]
    public float controlAreaHeight = 0.33f; // Нижняя треть экрана (значение по умолчанию)

    private void Update()
    {
        if (InputStarted() && IsInputInControlArea())
        {
            touched = true;
        }
        else if (InputEnded())
        {
            touched = false;
        }

        if (touched)
        {
            MoveBasket();
        }
    }

    // Проверяем, находится ли ввод в нижней части экрана для управления корзиной
    bool IsInputInControlArea()
    {
        Vector2 inputPos;

        if (Input.touchCount > 0)
        {
            inputPos = Input.GetTouch(0).position;
        }
        else
        {
            inputPos = Input.mousePosition;
        }

        // Проверяем, находится ли ввод в нижней части экрана (ниже controlAreaHeight)
        return inputPos.y < Screen.height * controlAreaHeight;
    }

    void MoveBasket()
    {
        Vector3 targetPosition = transform.position;

        // Продолжаем отслеживать ввод в нижней части экрана
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            // Проверяем, что касание всё ещё в зоне управления
            if (touch.position.y < Screen.height * controlAreaHeight)
            {
                inputPosition = GetCursorPosition(touch.position);
            }
        }
        else if (Input.GetMouseButton(0))
        {
            // Проверяем, что мышь всё ещё в зоне управления
            if (Input.mousePosition.y < Screen.height * controlAreaHeight)
            {
                inputPosition = GetCursorPosition(Input.mousePosition);
            }
        }

        targetPosition.x = inputPosition.x;
        float step = 30 * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
    }

    Vector3 GetCursorPosition(Vector3 input)
    {
        return Camera.main.ScreenToWorldPoint(new Vector3(input.x, input.y, 10));
    }

    private bool InputStarted()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return touch.phase == TouchPhase.Began && touch.position.y < Screen.height * controlAreaHeight;
        }

        return Input.GetMouseButtonDown(0) && Input.mousePosition.y < Screen.height * controlAreaHeight;
    }

    private bool InputEnded()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            return touch.phase == TouchPhase.Ended;
        }

        return Input.GetMouseButtonUp(0);
    }
}