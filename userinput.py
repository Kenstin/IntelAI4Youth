from tensorflow import keras
import sys
import pandas as pd
import numpy as np

model = keras.Sequential([
    keras.layers.Dense(39, kernel_initializer='normal', input_dim=40, activation='relu'),
    keras.layers.Dense(256, kernel_initializer='normal', activation='relu'),
    keras.layers.Dense(256, kernel_initializer='normal', activation='relu'),
    keras.layers.Dense(1, kernel_initializer='normal')
])

model.load_weights('Weights-025--1.94244.hdf5')


df = pd.DataFrame(np.zeros((1, 40)))

sys.argv.pop(0)

subjects = {
    'Matematyka': 0,
    'Informatyka': 1,
    'Fizyka': 2,
    'Biologia': 3,
    'Chemia': 4,
    'Historia': 5,
    'Polski': 6,
    'Wos': 7,
    'Jezyk obcy inny niz angielski': 8,
    'Jezyk angielski': 9,
    'Geografia': 10,
}

df[subjects[sys.argv[0]]] = 1
if sys.argv[1] != 0:
    df[subjects[sys.argv[1]] + 11] = 1

if sys.argv[2] != 0:
    df[subjects[sys.argv[2]] + 22] = 1


for i in range(3, 10):
    df[30+i] = sys.argv[i]

prediction = model.predict(df)
print(prediction)