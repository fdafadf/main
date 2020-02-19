from keras import *
import keras2onnx
import onnx
import tensorflow

model = tensorflow.keras.models.Sequential([
  tensorflow.keras.layers.Flatten(input_shape=(28, 28)),
  tensorflow.keras.layers.Dense(128, activation='relu'),
  tensorflow.keras.layers.Dropout(0.2),
  tensorflow.keras.layers.Dense(10, activation='softmax')
])

model.compile(optimizer='adam', loss='sparse_categorical_crossentropy', metrics=['accuracy'])
tensorflow.saved_model.save(model, 'C:\Deployment')
#tensorflow.keras.models.save_model(model,'C:\Deployment\model.tf', save_format='tf')
print('end')

'''
model = Sequential()
model.add(layers.Dense(200, activation='relu', input_dim=9))
model.add(layers.Dropout(0.2))
model.add(layers.Dense(125, activation='relu'))
model.add(layers.Dense(75, activation='relu'))
model.add(layers.Dropout(0.1))
model.add(layers.Dense(25, activation='relu'))
model.add(layers.Dense(3, activation='softmax'))
model.compile(loss='categorical_crossentropy', optimizer='rmsprop', metrics=['acc'])
#model.fit(numpy.array(inputs), numpy.array(outputs), epochs = 200)
#model.save('C:\Deployment\model.tf', save_format='tf')
tensorflow.keras.models.save_model(model,'C:\Deployment\model.tf', save_format='tf')
#onnx_model = keras2onnx.convert_keras(model, model.name)
#onnx.save_model(onnx_model, 'C:\Deployment\model.onnx')
'''