import os
os.environ['TF_CPP_MIN_LOG_LEVEL'] = '3'
import tensorflow as tf
from tensorflow import keras
import sys
import pandas as pd
import numpy as np
tf.logging.set_verbosity(tf.logging.ERROR)


directory = os.path.dirname(os.path.realpath(sys.argv[0]))

d = os.path.join(directory, 'Weights-035--0.18753.hdf5')

model = keras.Sequential([
    keras.layers.Dense(39, kernel_initializer='normal', input_dim=40, activation='relu'),
    keras.layers.Dense(512, kernel_initializer='normal', activation='relu'),
    keras.layers.Dense(1, kernel_initializer='normal')
])

model.compile(optimizer='Adam', loss='mean_squared_logarithmic_error')

model.load_weights(d)


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
    'Jezyk_obcy': 8,
    'Jezyk_angielski': 9,
    'Geografia': 10,
}

tab = []
if len(sys.argv) > 1:
    tab = sys.argv
else:
    tab = sys.argv[0].split()

df[subjects[tab[0]]] = 1

if tab[1] != '0':
    df[subjects[tab[1]] + 11] = 1

if tab[2] != '0':
    df[subjects[tab[2]] + 22] = 1


for i in range(3, 10):
    df[30+i] = tab[i]

prediction = model.predict(df)
print(prediction[0][0])
