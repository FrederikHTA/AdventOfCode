namespace csharp.LeetCode;

public class TwoPointer_MostWater
{
    public int MaxArea(int[] height)
    {
        int maxArea = 0;
        int left = 0;
        int right = height.Length - 1;

        while (left < right)
        {
            var leftHeight = height[left];
            var rightHeight = height[right];
            var area = (right - left) * Math.Min(leftHeight, rightHeight);
            if (area > maxArea)
            {
                maxArea = Math.Max(maxArea, area);
            }

            if (leftHeight < rightHeight)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return maxArea;
    }
}
