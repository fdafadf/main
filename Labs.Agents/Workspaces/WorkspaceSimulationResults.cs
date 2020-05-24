using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Labs.Agents
{
    public class WorkspaceSimulationResults : IEnumerable<SimulationResultsDescription>
    {
        WorkspaceItemsDirectory<SimulationResultsDescription> Descriptions;

        public WorkspaceSimulationResults(Workspace workspace, DirectoryInfo directoryInfo)
        {
            Descriptions = new WorkspaceItemsDirectory<SimulationResultsDescription>(workspace, directoryInfo, "{0}.desc.json");
        }

        public void Add(SimulationResults item)
        {
            item.Description.Length = item.Series.Values.Select(data => data.Count).Max();
            Descriptions.Add(item.Description);
            GetDataFile(item.Description).Serialize(item.Series);
        }

        public SimulationResults Get(SimulationResultsDescription description)
        {
            var data = GetDataFile(description).Deserialize<Dictionary<string, List<double>>>();
            return new SimulationResults(description, data);
        }

        public bool Remove(SimulationResultsDescription description)
        {
            bool result = Descriptions.Remove(description);
            GetDataFile(description).Delete();
            return result;
        }

        public IEnumerator<SimulationResultsDescription> GetEnumerator() => Descriptions.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Descriptions.GetEnumerator();

        private FileInfo GetDataFile(SimulationResultsDescription description)
        {
            return Descriptions.Directory.GetFile(string.Format("{0}.data.json", description.Name));
        }
    }
}
