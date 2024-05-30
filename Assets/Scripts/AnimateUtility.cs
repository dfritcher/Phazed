    using UnityEngine;
    using UnityEngine.UI;
    using System.Collections;
    using System.Collections.Generic;
    using Easing;
    using System.Linq;
    using TMPro;

    public class AnimateUtility : MonoBehaviour
    {
        #region Fields, Properties
        /// <summary>
        /// This is the default ease function we use for these animations.
        /// </summary>
        private static EaseDelegate _cubicInOut = null;

        private static AnimateUtility _instance = null;
        public static AnimateUtility Instance { get { return _instance; } }
        #endregion Fields, Properties (end)
        
        #region Delegates, Events
        public delegate void AnimateUtilityEvent();
        #endregion Delegates, Events (end)

        #region Initialization
        protected void Awake()
            {
                if (_instance != null && _instance != this)
                {
                    Destroy(this.gameObject);
                }
                else
                {
                    _instance = this;
                }
            _cubicInOut = Ease.GetDelegate(EaseEquation.Cubic, EaseType.EaseInOut);
        }
        #endregion Initialization (end)
       
        #region Animation Coroutines

        // NOTE: Try to keep this in alphabetical order!

        #region Alpha

        /// <summary>
        /// Animates the alpha of a graphic element.
        /// </summary>
        public IEnumerator AnimateGraphicAlpha(Graphic graphic, float targetAlpha, float duration, float delay)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!graphic) yield break;

            float counter = duration;
            float startAlpha = graphic.color.a;

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!graphic) yield break;

                Color color = graphic.color;
                color.a = Mathf.Lerp(startAlpha, targetAlpha, animValue);
                graphic.color = color;

                yield return null;
            }
            while (counter > 0);

        }

        /// <summary>
        /// Animates the alpha of a graphic element. NOTE: In it's current state, this would conflict with animating graphic color
        /// </summary>
        public IEnumerator AnimateAlpha(CanvasGroup canvasGroup, float targetAlpha, float duration, float delay, bool forceLinear, AnimationCurve curve)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!canvasGroup) yield break;

            float counter = duration;
            float startAlpha = canvasGroup.alpha;

            // Looping
            //int loopsRemaining = activeAnimation.LoopCount;
            bool playForward = true;

            // Ease or curve:
            EaseDelegate ease = _cubicInOut;
            bool useCurve = (curve != null);

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                //counter -= (activeAnimation.UseUnscaledTime) ? Time.unscaledDeltaTime : Time.deltaTime;
                float percent = (duration - counter) / duration;
                if (!playForward) percent = 1f - percent;

                //float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!canvasGroup) yield break;

                // Animate using the given curve:
                if (useCurve)
                {
                    float animValue = curve.Evaluate(percent);
                    canvasGroup.alpha = startAlpha + ((targetAlpha - startAlpha) * animValue);
                }
                // Animate linearly:
                else if (forceLinear) canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, percent);
                // Animate using ease equation:
                else
                {
                    float animValue = (float)ease.Invoke(percent, 0f, 1f, 1f);
                    canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, animValue);
                }

                //canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, animValue);

                yield return null;

                // Check for loop:
                //if (counter <= 0 && activeAnimation != null && activeAnimation.LoopType != LoopType.None)
                //{
                //    // Want to reset the counter if loops remaining are greater than or less than 0:
                //    // (NOTE: For continuous loops, this count is set to -1 so that's why we check it this way)
                //    if (loopsRemaining != 0)
                //    {
                //        loopsRemaining--;
                //        counter = duration;
                //    }

                //    // If ping pong, toggle play foward/backward:
                //    if (activeAnimation.LoopType == LoopType.PingPong) playForward = !playForward;
                //}
            }
            while (counter > 0);
            
        }

        /// <summary>
        /// Animates the alpha of a graphic element. NOTE: In it's current state, this would conflict with animating graphic color
        /// </summary>
        public IEnumerator AnimateAlpha(SpriteRenderer spriteRenderer, float targetAlpha, float duration, float delay)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!spriteRenderer) yield break;

            float counter = duration;
            float startAlpha = spriteRenderer.color.a;

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!spriteRenderer) yield break;

                Color color = spriteRenderer.color;
                color.a = Mathf.Lerp(startAlpha, targetAlpha, animValue);
                spriteRenderer.color = color;

                yield return null;
            }
            while (counter > 0);        
        }

        /// <summary>
        /// Animates the alpha of a mesh renderer. NOTE: In it's current state, this would conflict with animating graphic color
        /// </summary>
        public IEnumerator AnimateAlpha(MeshRenderer meshRenderer, float targetAlpha, float duration, float delay)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!meshRenderer) yield break;

            float counter = duration;
            float startAlpha = meshRenderer.material.color.a;

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!meshRenderer) yield break;

                Color color = meshRenderer.material.color;
                color.a = Mathf.Lerp(startAlpha, targetAlpha, animValue);
                meshRenderer.material.color = color;

                yield return null;
            }
            while (counter > 0);
        }
        #endregion Alpha (end)

        #region Color Value
        /// <summary>
        /// Animates the color of a graphic element.
        /// </summary>
        public static IEnumerator AnimateGraphicColor(Graphic graphic, Color targetColor, float duration, float delay, AnimateUtilityEvent callback)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!graphic) yield break;

            float counter = duration;
            Color startColor = graphic.color;

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!graphic) yield break;

                graphic.color = Color.Lerp(startColor, targetColor, animValue);

                yield return null;
            }
            while (counter > 0);
            callback?.Invoke();
    
        }
        /// <summary>
        /// Animates the color of a text mesh element.
        /// </summary>
        private IEnumerator AnimateTextMeshColor(TMP_Text textMesh, Color targetColor, float duration, float delay)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!textMesh) yield break;

            float counter = duration;
            Color startColor = textMesh.color;

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!textMesh) yield break;

                textMesh.color = Color.Lerp(startColor, targetColor, animValue);

                yield return null;
            }
            while (counter > 0);

        }
        
        #endregion Color Value (end)

        #region Pivot

        /// <summary>
        /// Animates the pivot of a rect transform
        /// </summary>
        public IEnumerator AnimatePivot(RectTransform rectTransform, Vector2 targetPivot, float duration, float delay)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!rectTransform) yield break;

            float counter = duration;
            Vector2 startPivot = rectTransform.pivot;

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!rectTransform) yield break;

                rectTransform.pivot = Vector2.Lerp(startPivot, targetPivot, animValue);

                yield return null;
            }
            while (counter > 0);        
        }
        
        #endregion Pivot (end)

        #region Position Values

        #region Anchored Position
   
        /// <summary>
        /// Animates the anchored position property of a RectTransform.
        /// </summary>
        public IEnumerator AnimateAnchoredPosition(RectTransform rectTransform, Vector3 targetPos, float duration, float delay, AnimationCurve curve)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!rectTransform) yield break;

            float counter = duration;
            Vector3 startPos = rectTransform.anchoredPosition;
            bool useCurve = (curve != null);

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                //float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!rectTransform) yield break;

                Vector3 newPosition = transform.localPosition;
                if (useCurve)
                {
                    float animValue = curve.Evaluate(percent);
                    newPosition = startPos + ((targetPos - startPos) * animValue);
                }
                else
                {
                    float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);
                    newPosition = Vector3.Lerp(startPos, targetPos, animValue);
                }
                rectTransform.anchoredPosition = newPosition;

                //rectTransform.anchoredPosition = Vector3.Lerp(startPos, targetPos, animValue);

                yield return null;
            }
            while (counter > 0);        
        }
        /// <summary>
        /// Animates the anchored position y property of a RectTransform.
        /// </summary>
        public IEnumerator AnimateAnchoredPositionY(RectTransform rectTransform, float targetY, float duration, float delay)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!rectTransform) yield break;

            float counter = duration;
            float startY = rectTransform.anchoredPosition.y;

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!rectTransform) yield break;

                float currentX = rectTransform.anchoredPosition.x;

                rectTransform.anchoredPosition = new Vector2(currentX, Mathf.Lerp(startY, targetY, animValue));

                yield return null;
            }
            while (counter > 0);

        }
        
        #endregion Anchored Position (end)

        #region Local Position

        /// <summary>
        /// Animates the local position property of a Transform.
        /// </summary>
        public IEnumerator AnimateLocalPosition(Transform transform, Vector3 targetPos, float duration, float delay, AnimationCurve curve)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!transform) yield break;

            float counter = duration;
            Vector3 startPos = transform.localPosition;
            bool useCurve = (curve != null);

            Vector3 newPos = Vector3.zero;

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                //float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!transform) yield break;

                //transform.localPosition = Vector3.Lerp(startPos, targetPos, animValue);

                newPos = transform.localPosition;
                if (useCurve)
                {
                    float animValue = curve.Evaluate(percent);
                    newPos = startPos + ((targetPos - startPos) * animValue);
                }
                else
                {
                    float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);
                    newPos = Vector3.Lerp(startPos, targetPos, animValue);
                }
                transform.localPosition = newPos;

                yield return null;
            }
            while (counter > 0);

            //if (transform) transform.localPosition = targetPos;        
        }

        /// <summary>
        /// Animates the local position property of a Transform on the X and Z axis.
        /// </summary>
        public IEnumerator AnimatePositionXZ(Transform transform, Vector3 targetPos, float duration, float delay, AnimationCurve curve)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!transform) yield break;

            float counter = duration;
            Vector3 startPos = transform.localPosition;
            bool useCurve = (curve != null);

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;

                if (!transform) yield break;

                targetPos.y = transform.localPosition.y;
                startPos.y = transform.localPosition.y;
                Vector3 newPosition = transform.localPosition;
                if (useCurve)
                {
                    float animValue = curve.Evaluate(percent);
                    newPosition = startPos + ((targetPos - startPos) * animValue);
                }
                else
                {
                    float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);
                    newPosition = Vector3.Lerp(startPos, targetPos, animValue);
                }
                transform.localPosition = newPosition;

                yield return null;
            }
            while (counter > 0);
        
        }

        /// <summary>
        /// Animates the local position property of a Transform on the Y axis.
        /// </summary>
        public IEnumerator AnimatePositionY(Transform transform, float targetY, float endY, float duration, float delay, AnimationCurve curve)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!transform) yield break;

            float counter = duration;
            float startY = transform.localPosition.y;
            bool useCurve = (curve != null);

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;

                if (!transform) yield break;

                Vector3 newPosition = transform.localPosition;
                if (useCurve)
                {
                    float animValue = curve.Evaluate(percent);
                    newPosition.y = startY + ((targetY - startY) * animValue);
                }
                else
                {
                    float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);
                    newPosition.y = Mathf.Lerp(startY, targetY, animValue);
                }
                transform.localPosition = newPosition;

                yield return null;
            }
            while (counter > 0);        
        }
       
        #endregion Local Position (end)

        #region Position

        /// <summary>
        /// Animates the position property of a Transform.
        /// </summary>
        public IEnumerator AnimatePosition(Transform transform, Vector3 targetPos, float duration, float delay, AnimationCurve curve)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!transform) yield break;

            float counter = duration;
            Vector3 startPos = transform.position;
            bool useCurve = (curve != null);

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                //float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!transform) yield break;

                //transform.position = Vector3.Lerp(startPos, targetPos, animValue);

                Vector3 newPosition = transform.localPosition;
                if (useCurve)
                {
                    float animValue = curve.Evaluate(percent);
                    newPosition = startPos + ((targetPos - startPos) * animValue);
                }
                else
                {
                    float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);
                    newPosition = Vector3.Lerp(startPos, targetPos, animValue);
                }
                transform.position = newPosition;

                yield return null;
            }
            while (counter > 0);

        }
        

        #endregion Position (end)

        #endregion Position Values (end)

        #region Rotation Values
    
        /// <summary>
        /// Animates the position property of a Transform.
        /// </summary>
        public IEnumerator AnimateRotation(Transform transform, Quaternion targetRotation, float duration, float delay, AnimationCurve curve)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!transform) yield break;

            float counter = duration;
            Quaternion startRotation = transform.rotation;
            bool useCurve = (curve != null);

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                float animValue = 0;
                //float animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);

                if (!transform) yield break;

                //transform.position = Vector3.Lerp(startPos, targetPos, animValue);

                Quaternion newRotation = transform.rotation;
                if (useCurve)
                {
                    animValue = curve.Evaluate(percent);
                    //newRotation = startRotation + ((targetRotation - startRotation) * animValue);
                }
                else
                {
                    animValue = (float)_cubicInOut.Invoke((double)percent, 0, 1, 1);
                    //newRotation = Quaternion.Lerp(startRotation, targetRotation, animValue);
                }
                newRotation = Quaternion.Lerp(startRotation, targetRotation, animValue);
                transform.rotation = newRotation;

                yield return null;
            }
            while (counter > 0);        
        }
        
        #endregion Rota    tion Values (end)

        #region Scale
        
        /// <summary>
        /// Animates the scale property of a Transform.
        /// </summary>
        public IEnumerator AnimateScale(Transform transform, Vector3 targetScale, float duration, float delay, bool forceLinear, AnimationCurve curve)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!transform) yield break;

            float counter = duration;
            Vector3 startScale = transform.localScale;

            // Looping
            //int loopsRemaining = activeAnimation.LoopCount;
            bool playForward = true;

            // Ease or curve:
            EaseDelegate ease = _cubicInOut;
            bool useCurve = (curve != null);

            // Animate values over time:
            do
            {
                 counter -= Time.deltaTime;
                //counter -= (activeAnimation.UseUnscaledTime) ? Time.unscaledDeltaTime : Time.deltaTime;
                float percent = (duration - counter) / duration;
                if (!playForward) percent = 1f - percent;

                if (!transform) yield break;

                // Animate using the given curve:
                if (useCurve)
                {
                    //start + ((end - start) * Curve.Evaluate(value)); // Use this instead of lerp! (lerp is clamped from 0 to 1)
                    // Did this manually because Lerp percent has max input of 1f, so this gives us more flexability with the animation curve
                    float animValue = curve.Evaluate(percent);
                    transform.localScale = startScale + ((targetScale - startScale) * animValue);
                }
                // Animate linearly:
                else if (forceLinear) transform.localScale = Vector3.Lerp(startScale, targetScale, percent);
                // Animate using ease equation:
                else
                {
                    float animValue = (float)ease.Invoke(percent, 0f, 1f, 1f);
                    transform.localScale = Vector3.Lerp(startScale, targetScale, animValue);
                }

                yield return null;

                //// Check for loop:
                //if (counter <= 0 && activeAnimation != null && activeAnimation.LoopType != LoopType.None)
                //{
                //    // Want to reset the counter if loops remaining are greater than or less than 0:
                //    // (NOTE: For continuous loops, this count is set to -1 so that's why we check it this way)
                //    if (loopsRemaining != 0)
                //    {
                //        loopsRemaining--;
                //        counter = duration;
                //    }

                //    // If ping pong, toggle play foward/backward:
                //    if (activeAnimation.LoopType == LoopType.PingPong) playForward = !playForward;
                //}
            }
            while (counter > 0);

            
        }
       
        #endregion Scale (end)

        #region Size Delta (RectTransform)

        #region Private
        /// <summary>
        ///  Animates the size delta property of a RectTransform.
        /// </summary>
        private IEnumerator AnimateSizeDelta(RectTransform rectTransform, Vector2 targetSizeDelta, float duration, float delay, bool forceLinear, AnimationCurve curve)
        {
            if (delay > 0) yield return new WaitForSeconds(delay);

            if (!transform) yield break;

            float counter = duration;
            Vector2 startSizeDelta = rectTransform.sizeDelta;

            // Looping
            //int loopsRemaining = activeAnimation.LoopCount;
            bool playForward = true;

            // Ease or curve:
            EaseDelegate ease = _cubicInOut;
            bool useCurve = (curve != null);

            // Animate values over time:
            do
            {
                counter -= Time.deltaTime;
                float percent = (duration - counter) / duration;
                if (!playForward) percent = 1f - percent;

                if (!rectTransform) yield break;

                // Animate using the given curve:
                if (useCurve)
                {
                    //start + ((end - start) * Curve.Evaluate(value)); // Use this instead of lerp! (lerp is clamped from 0 to 1)
                    // Did this manually because Lerp percent has max input of 1f, so this gives us more flexability with the animation curve
                    float animValue = curve.Evaluate(percent);
                    rectTransform.sizeDelta = startSizeDelta + ((targetSizeDelta - startSizeDelta) * animValue);
                }
                // Animate linearly:
                else if (forceLinear) rectTransform.sizeDelta = Vector2.Lerp(startSizeDelta, targetSizeDelta, percent);
                // Animate using ease equation:
                else
                {
                    float animValue = (float)ease.Invoke(percent, 0f, 1f, 1f);
                    rectTransform.sizeDelta = Vector2.Lerp(startSizeDelta, targetSizeDelta, animValue);
                }

                yield return null;

                //// Check for loop:
                //if (counter <= 0 && activeAnimation != null && activeAnimation.LoopType != LoopType.None)
                //{
                //    // Want to reset the counter if loops remaining are greater than or less than 0:
                //    // (NOTE: For continuous loops, this count is set to -1 so that's why we check it this way)
                //    if (loopsRemaining != 0)
                //    {
                //        loopsRemaining--;
                //        counter = duration;
                //    }

                //    // If ping pong, toggle play foward/backward:
                //    if (activeAnimation.LoopType == LoopType.PingPong) playForward = !playForward;
                //}
            }
            while (counter > 0);

        }
        #endregion Private (end)

        #endregion Size Delta (RectTransform) (end)

        #endregion Animations Coroutines (end)
    }
