using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Ejercicio2._4_Grupo2.Controllers
{

    public class VideoController
    {
        readonly SQLiteAsyncConnection _connection;

        public VideoController()
        {
            SQLiteOpenFlags extensiones = SQLiteOpenFlags.ReadWrite |
                                          SQLiteOpenFlags.Create |
                                          SQLiteOpenFlags.SharedCache;

            _connection = new SQLiteAsyncConnection(Path.Combine(FileSystem.AppDataDirectory, "DBVideo.db3"), extensiones);
            _connection.CreateTableAsync<Models.Video>();
        }


        //CRUD Methods
        //Create
        public async Task<int> Store(Models.Video video)
        {
            if (video.Id == 0)
            {
                return await _connection.InsertAsync(video);
            }
            else
            {
                return await _connection.UpdateAsync(video);
            }
        }

        //Read
        public async Task<List<Models.Video>> GetList()
        {
            return await _connection.Table<Models.Video>().ToListAsync();
        }
    }
}
