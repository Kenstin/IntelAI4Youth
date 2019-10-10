import pandas as pd

df = pd.read_csv("googledata.csv")

df = df[['subject_1',
         'subject_2',
         'subject_3',
         'score_1',
         'score_2',
         'score_3',
         'coffees',
         'energy_drinks',
         'hours_of_sleep',
         'study_time',
         'stress_level']]


def is_nan(num):
    return num != num


def get_rid_of_percent_symbol(x):  # some inputs were e.g '90%', and we need the value rather than some percentage
    x = str(x)
    if x[len(x)-1] == '%':
        return x[:-1]
    else:
        return x


def get_rid_of_ranges(x):  # some inputs were like '50-80', this function will get the mean of given range
    x = str(x)
    if x.rfind('-') != -1:
        x = x.split('-')
        return int((int(x[0]) + int(x[1])) / 2)
    elif x.rfind('/') != -1:
        return int((int(x[0]) + int(x[1])) / 2)
    else:
        return x


def get_rid_of_scores_lower_than_10(x):  # some inputs were like '5',I assume it was meant to be '50' and multiply by 10
    if x != 'nan':
        x = int(x)
        if x < 10:
            return x*10
        else:
            return x
    else:
        return x


for i in range(1, 4):
    df['score_' + str(i)] = df.apply(lambda row: get_rid_of_percent_symbol(row['score_' + str(i)]), axis=1)
    df['score_' + str(i)] = df.apply(lambda row: get_rid_of_ranges(row['score_' + str(i)]), axis=1)
    df['score_' + str(i)] = df.apply(lambda row: get_rid_of_scores_lower_than_10(row['score_' + str(i)]), axis=1)


df.to_csv('preprocesseddata.csv', index=False, sep=',', encoding='utf-8')
