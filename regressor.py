import pandas as pd
from tensorflow import keras
from sklearn.preprocessing import StandardScaler


train = pd.read_csv('preprocesseddata.csv')

targets = pd.read_csv('targets.csv')


# declaring our NN model
model = keras.Sequential([
    keras.layers.Dense(39, kernel_initializer='normal', input_dim=40, activation='relu'),
    keras.layers.Dense(256, kernel_initializer='normal', activation='relu'),
    keras.layers.Dense(256, kernel_initializer='normal', activation='relu'),
    keras.layers.Dense(1, kernel_initializer='normal')
])

# compiling the model
model.compile(optimizer='Adam', loss='mean_absolute_error')

# defining checkpoint callback
checkpoint_name = 'Weights-{epoch:03d}--{val_loss:.5f}.hdf5'
checkpoint = keras.callbacks.ModelCheckpoint(checkpoint_name,
                                             monitor='val_loss',
                                             verbose=1,
                                             save_best_only=True,
                                             mode='auto')
callbacks_list = [checkpoint]
# train the model, callbacks to be added later
model.fit(train, targets, epochs=500, validation_split=0.2, callbacks=callbacks_list)
