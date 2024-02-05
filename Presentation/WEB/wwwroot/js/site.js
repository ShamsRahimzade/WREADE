// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
<script>
    document.getElementById('authorButton').addEventListener('click', function () {
        // Burada kullanıcı "Author" butonuna tıkladığında yapılması gereken işlemleri tanımlayabilirsiniz.
        // Örneğin, kullanıcıyı uygun bir sayfaya yönlendirebilirsiniz.
        
        // Örnek olarak, sayfayı yönlendirme:
        window.location.href = '/Author/Index'; // Sayfa yolu projenize göre değişebilir.
    });
</script>