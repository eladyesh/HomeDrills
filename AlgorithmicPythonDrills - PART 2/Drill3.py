def is_sorted_palindrome(s) -> bool:
    """
    Check if a string is a sorted palindrome.

    Parameters:
    s (str): The input string to be checked.

    Returns:
    bool: True if the string is a sorted palindrome, False otherwise.
    """
    
    # Check if the string is a palindrome
    if s != s[::-1]:
        return False

    # Check if the string is sorted in both halves
    mid = len(s) // 2
    left_half = s[:mid]
    right_half = s[mid:]

    # Sort the left half in ascending order and the right half in descending order
    sorted_left = ''.join(sorted(left_half))
    sorted_right = ''.join(sorted(right_half, reverse=True))

    # Compare the sorted halves with the original halves
    if sorted_left == left_half and sorted_right == right_half:
        return True
    else:
        return False


if __name__ == "__main__":

    # Test cases
    print(is_sorted_palindrome("שוש"))  # False
    print(is_sorted_palindrome("אבגדגבא"))  # True
    print(is_sorted_palindrome("pbp"))  # False
    print(is_sorted_palindrome("abcdcba"))  # True
