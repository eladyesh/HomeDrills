import matplotlib.pyplot as plt
import numpy as np


def main() -> None:
    
    """
    Main function to collect input numbers, calculate statistics (average, number of positive numbers, sorted numbers
    in ascending order), and display results.
    """
    
    numbers = []

    # Input numbers from user
    while True:
        num = input("Enter a number (press -1 to finish input): ")
        if num == '-1':
            break
        numbers.append(float(num))

    if not numbers:
        print("No numbers were entered.")
        return

    # Calculate statistics
    average = sum(numbers) / len(numbers)
    positive_count = sum(1 for num in numbers if num > 0)
    sorted_numbers = sorted(numbers)

    # Print the required statistics
    print(f"The average of the numbers is: {average}")
    print(f"The count of positive numbers is: {positive_count}")
    print("The sorted numbers in ascending order are:")
    print(sorted_numbers)

    # Bonus: Plotting the data points
    x_values = range(1, len(numbers) + 1)
    plt.scatter(x_values, numbers)
    plt.xlabel('Order of Input')
    plt.ylabel('Number')
    plt.title('Input Numbers Plot')
    plt.show()

    # Bonus: Calculate Pearson correlation coefficient
    pearson_coefficient = np.corrcoef(x_values, numbers)[0, 1]
    print("Pearson correlation coefficient:", pearson_coefficient)


if __name__ == "__main__":
    
    main()
