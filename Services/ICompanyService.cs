using JEX_backend.Models;
public interface ICompanyService
{
    Task<List<Company>> GetCompaniesWithJobopeningsAsync();
    /// <summary>
    /// Get all companies that have at least one jobopening which is active
    /// </summary>
    /// <returns></returns>
    Task<List<Company>> GetCompaniesAsync();

    /// <summary>
    /// Get a company by it's <see cref="Guid"/> Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Company> GetCompanyAsync(Guid id);

    /// <summary>
    /// Create a new company
    /// </summary>
    /// <param name="company"></param>
    /// <returns></returns>
    Task<Company> CreateCompanyAsync(Company company);

    /// <summary>
    /// Update a company
    /// </summary>
    /// <param name="id"></param>
    /// <param name="company"></param>
    /// <returns></returns>
    Task<Company> UpdateAsync(Guid id, Company company);

    /// <summary>
    /// Delete a company
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(Guid id);

    /// <summary>
    /// Create a jobopening on a company
    /// </summary>
    /// <param name="jobOpening"></param>
    /// <returns></returns>
    Task<JobOpening> CreateJobOpeningAsync(JobOpening jobOpening);
}