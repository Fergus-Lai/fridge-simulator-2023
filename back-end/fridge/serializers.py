from fridge.models import User, Freezer_Item, Fridge_Item
from rest_framework import serializers

    

class FridgeSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Fridge_Item
        fields = ['id', 'name', 'type', 'exp_date']


class FreezerSerializer(serializers.HyperlinkedModelSerializer):
    class Meta:
        model = Freezer_Item
        fields = ['id', 'name', 'type', 'exp_date']
        
class UserSerializer(serializers.HyperlinkedModelSerializer):
    
    fridge = FridgeSerializer(many=True)
    freezer = FreezerSerializer(many=True)
    
    class Meta:
        model = User
        fields = ['id', 'name', 'fridge', 'freezer']
        
    def create(self, validated_data):
        fridge_items = validated_data.pop('fridge')
        freezer_items = validated_data.pop('freezer')
        user = User.objects.create(**validated_data)
        for fridge in fridge_items:
            Fridge_Item.objects.create(user=user, **fridge)
        for freezer in freezer_items:
            Freezer_Item.objects.create(user=user, **freezer)
        return user
    def update(self, instance, validated_data):
        fridge_items = validated_data.pop('fridge')
        freezer_items = validated_data.pop('freezer')
        for fridge in fridge_items:
            Fridge_Item.objects.create(user=instance, **fridge)
        for freezer in freezer_items:
            Freezer_Item.objects.create(user=instance, **freezer)
        return instance