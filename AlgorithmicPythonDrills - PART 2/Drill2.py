def pythagorean_triplet_by_sum(sum: int):
    """Prints all Pythagorean triplets that sum up to a given number.

    Args:
        sum (int): The desired sum of the Pythagorean triplets.

    Returns:
        None
    """

    # We can solve the equation:
    # 1. ---> (a ** 2 + b ** 2 = c ** 2) => (a ** 2 + b ** 2 = (sum - a - b) * (sum - a - b)
    # 2. ---> a ** 2 + b ** 2 = sum ** 2 - a * sum - b * sum - a * sum + a ** 2 + a * b - b * sum  + a * b + b ** 2
    # 3. ---> 2 * b * sum - 2 * a * b = sum ** 2 - 2 * a * sum
    # 4. ---> b = (sum ** 2 - 2 * a * sum) / (2 * (sum - a))

    # We start by iterating over the numbers from the smallest number (a) till sum.
    # Then, we compute b (the value from our solved equation)
    # Check if c (which is sum - a - b) is bigger than a and b and if a ** 2 + b ** 2 == c ** 2

    for a in range(1, sum):
        b = int((sum ** 2 - 2 * a * sum) / (2 * (sum - a)))
        c = sum - a - b
        if c > b > a:
            if a ** 2 + b ** 2 == c ** 2:
                print(f"{a} < {b} < {c}")


if __name__ == "__main__":
    pythagorean_triplet_by_sum(12)  # 3 < 4 < 5
    pythagorean_triplet_by_sum(120)  # 20 < 48 < 52, 24 < 45 < 51, 30 < 40 < 50
