using System;
using System.Net.Cache;

namespace OrderSystem.Domain.Entities;

public class Address : Entity
{
    // Propriedades principais
    public required string FullName { get; set; }
    public required string Cpf { get; set; }
    public required string Street { get; set; }
    public required string Number { get; set; }
    public string Complement { get; set; } = string.Empty;
    public required string Neighborhood { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string ZipCode { get; set; }
    public required string Country { get; set; } = "Brasil"; // Valor padrão
    public Guid UserId { get; set; }
    public User? User { get; set; }
    public List<Order>? Orders { get; set; }

    // Construtor vazio (importante para serialização/EF)
    protected Address() { }

    // Construtor para facilitar a criação
    public Address(
        string fullName,
        string cpf,
        string street,
        string number,
        string zipCode,
        string city,
        string state)
    {
        FullName = fullName;
        Cpf = cpf;
        Street = street;
        Number = number;
        ZipCode = zipCode;
        City = city;
        State = state;
    }

    // Método útil para exibir o endereço formatado
    public override string ToString()
    {
        return $"{FullName} {Cpf} {Street}, {Number} - {Neighborhood}, {City}/{State} - CEP: {ZipCode}";
    }

}
