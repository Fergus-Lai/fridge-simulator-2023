from fridge.models import User, Freezer_Item, Fridge_Item
from rest_framework import serializers

class UserSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = User
        fields = ['id', 'name']
    

class FridgeSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Fridge_Item
        fields = ['id', 'name', 'type', 'exp_date','user']


class FreezerSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Freezer_Item
        fields = ['id', 'name', 'type', 'exp_date','user']