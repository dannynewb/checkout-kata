# Checkout Kata

Basic checkout and offer functionality.

The basket accepts item names only and reads the item prices from a respository. At this point it is presumed the items are coming from a client and accepting price data would leave the basket open to exploitation.

Offers are setup based on a price reduction on multiples of an item purchased, e.g
| Item Name | Cost | Offer Multiple | Cost Without Offer | Price Reduction | Cost With Offer |
|:---------:|:----:|:--------------:|:------------------:|:---------------:|:---------------:|
|     B     |  15  |        3       |         45         |        5        |        40       |
|     D     |  55  |        2       |         110        |      27.50      |      82.50      |
