echo "Iniciando o Minikube..."
minikube delete
minikube start

echo ""
echo "Aplicando as configurações do Kubernetes..."
kubectl apply -f k8s-contactzone/

echo ""
echo "Criando um túnel para os serviços..."
echo "Pressione Ctrl+C para encerrar o túnel quando não for mais necessário."
minikube tunnel > /dev/null 2>&1 &

echo ""
echo "Os serviços estarão disponíveis (após 1m) nas seguintes URLs:"
echo "CriarContato: http://localhost:8080/swagger/index.html"
echo "AtualizarContato: http://localhost:8081/swagger/index.html"
echo "DeletarContato: http://localhost:8082/swagger/index.html"
echo "RabbitMQ: http://localhost:15672"