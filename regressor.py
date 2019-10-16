import pandas as pd
from tensorflow import keras
from sklearn.preprocessing import StandardScaler


df = pd.read_csv('preprocesseddata.csv')

ss = StandardScaler()

subjects = df[['subject_1',
               'subject_2',
               'subject_3']]

subjects = pd.get_dummies(subjects)   # making 'subjects' dataframe one-hot encoded

features = df[['score_1',   # defining features of the dataset
               'score_2',
               'score_3',
               'coffees',
               'energy_drinks',
               'hours_of_sleep',
               'study_time']]

scaled_features = ss.fit_transform(features)   # scaling the features
scaled_features_df = pd.DataFrame(scaled_features, index=features.index, columns=features.columns)
targets = df[['stress_level']]  # declaring targets

# merging subjects dataframe with features dataframe
train = pd.concat([subjects, features], axis=1, sort=False, ignore_index=True)
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
