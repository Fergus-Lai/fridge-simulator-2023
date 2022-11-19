from django.http import HttpResponse, JsonResponse
from django.views.decorators.csrf import csrf_exempt
from rest_framework.parsers import JSONParser
from fridge.models import User, Fridge_Item, Freezer_Item
from fridge.serializers import UserSerializer, FreezerSerializer, FridgeSerializer
from uuid import uuid4

# TODO: Delete method for user
# TODO: GET, POST, PUT, DELETE for Freezer Items and Fridge Items

@csrf_exempt
def user(request):
    data = JSONParser().parse(request)
    if request.method == "POST":
        id = uuid4()
        data["id"] = str(id)
        serializer = UserSerializer(data=data)
        if serializer.is_valid():
            serializer.save()
            return JsonResponse(serializer.data)
        return JsonResponse(serializer.errors, status=400)
    try:
        user = User.objects.get(pk=data["id"])
    except User.DoesNotExist:
        return HttpResponse(status=404)
    if request.method == 'GET':
        serializer = UserSerializer(user)
        return JsonResponse(serializer.data, safe=False)
    elif request.method == 'PUT':
        data = JSONParser().parse(request)
        serializer = UserSerializer(user, data=data)
        if serializer.is_valid():
            serializer.save()
            return JsonResponse(serializer.data)
        return JsonResponse(serializer.errors, status=400)

@csrf_exempt
def fridge(request):
    data = JSONParser().parse(request)

    try:
        user = User.objects.get(pk=data["id"])
    except User.DoesNotExist:
        return HttpResponse(status=404)

    if request.method == "GET":
        items = Fridge_Item.objects.filter(user=user).values().order_by("exp_date")
        results = {k: v for k, v in enumerate(items)}
        return JsonResponse(results)

    elif request.method == 'PUT':
        data = JSONParser().parse(request)
        serializer = FridgeSerializer(fridge, data=data)
        if serializer.is_valid():
            serializer.save()
            return JsonResponse(serializer.data)
        return JsonResponse(serializer.errors, status=400)

    elif request.method == "POST":
        id = uuid4()
        data["id"] = str(id)
        serializer = FridgeSerializer(data=data)
        if serializer.is_valid():
            serializer.save()
            return JsonResponse(serializer.data)
        return JsonResponse(serializer.errors, status=400)


@csrf_exempt
def freezer(request):
    data = JSONParser().parse(request)

    try:
        user = User.objects.get(pk=data["id"])
    except User.DoesNotExist:
        return HttpResponse(status=404)

    if request.method == "GET":
        items = Freezer_Item.objects.filter(user=user).values().order_by("exp_date")
        results = {k: v for k, v in enumerate(items)}
        return JsonResponse(results)

    elif request.method == 'PUT':
        data = JSONParser().parse(request)
        serializer = FreezerSerializer(freezer, data=data)
        if serializer.is_valid():
            serializer.save()
            return JsonResponse(serializer.data)
        return JsonResponse(serializer.errors, status=400)

    elif request.method == "POST":
        id = uuid4()
        data["id"] = str(id)
        serializer = FreezerSerializer(data=data)
        if serializer.is_valid():
            serializer.save()
            return JsonResponse(serializer.data)
        return JsonResponse(serializer.errors, status=400)
