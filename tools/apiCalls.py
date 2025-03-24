import requests
import random
import string
import time

# Base URL of the API
BASE_URL = "https://goodcopy.forti-erebor.ovh/api/product"

# Function to generate a random string
def random_string(length=10):
    return ''.join(random.choices(string.ascii_letters, k=length))

# Function to generate a random price
def random_price():
    return round(random.uniform(1.0, 1000.0), 2)

# Function to generate a random stock
def random_stock():
    return random.randint(1, 100)

# Function to create a new product
def create_product():
    product = {
        "name": random_string(),
        "price": random_price(),
        "stock": random_stock()
    }
    response = requests.post(BASE_URL, json=product)
    return response

# Function to get a product by ID
def get_product_by_id(product_id):
    response = requests.get(f"{BASE_URL}/{product_id}")
    return response

# Function to update a product
def update_product(product_id):
    product = {
        "id": product_id,
        "name": random_string(),
        "price": random_price(),
        "stock": random_stock()
    }
    response = requests.put(BASE_URL, json=product)
    return response

# Function to delete a product
def delete_product(product_id):
    response = requests.delete(f"{BASE_URL}/{product_id}")
    return response

# Function to get all products
def get_all_products():
    response = requests.get(BASE_URL)
    return response

# Main function to make random API requests
def make_random_requests():
    created_product_ids = []

    try:
        while True:
            action = random.choice(["create", "get_all", "get", "update", "delete"])
            response = None  # Initialize response to avoid UnboundLocalError
            method = ""
            url = ""

            if action == "create":
                response = create_product()
                method = "POST"
                url = BASE_URL
                if response.status_code == 201:
                    created_product_ids.append(response.json().get("id"))

            elif action == "get_all":
                response = get_all_products()
                method = "GET"
                url = BASE_URL

            elif action == "get" and created_product_ids:
                product_id = random.choice(created_product_ids)
                response = get_product_by_id(product_id)
                method = "GET"
                url = f"{BASE_URL}/{product_id}"

            elif action == "update" and created_product_ids:
                product_id = random.choice(created_product_ids)
                response = update_product(product_id)
                method = "PUT"
                url = BASE_URL

            elif action == "delete" and created_product_ids:
                product_id = random.choice(created_product_ids)
                response = delete_product(product_id)
                method = "DELETE"
                url = f"{BASE_URL}/{product_id}"
                if response.status_code == 204:
                    created_product_ids.remove(product_id)

            if response is not None:
                print(f"{method}: {url} | {response.status_code} {response.reason}")

            time.sleep(1)  # Wait for 1 second before the next request

    except KeyboardInterrupt:
        print("Requests stopped.")

if __name__ == "__main__":
    make_random_requests()
