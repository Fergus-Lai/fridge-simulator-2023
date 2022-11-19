from django.urls import include, path
from fridge import views

# Wire up our API using automatic URL routing.
# Additionally, we include login URLs for the browsable API.
urlpatterns = [
    path('user/', views.user),
    # path('freezer/',views.freezer)
    path('fridge/',views.fridge),
    path('webpush/', include('webpush.urls'))
]