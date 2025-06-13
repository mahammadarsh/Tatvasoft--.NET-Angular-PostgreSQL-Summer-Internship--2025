import { HttpClient } from "@angular/common/http"
import { Injectable } from "@angular/core"
import { Observable } from "rxjs"
import { environment } from "../../../environments/environment"
import { User, UserDetail } from "../interfaces/user.interface"
import { API_ENDPOINTS } from "../constants/api.constants"
import { Mission } from "../interfaces/common.interface"

@Injectable({
  providedIn: "root",
})
export class ClientService {
  constructor(private http: HttpClient) {}
  apiUrl = `${environment.apiBaseUrl}/api`;
  imageUrl = environment.apiBaseUrl;
  
  //ShareYourStory
  missionTitleList(): Observable<Mission[]> {
    return this.http.get<Mission[]>(`${this.apiUrl + API_ENDPOINTS.CLIENT_MISSION.MISSION_TITLE}`)
  }

  loginUserDetailById(id: any): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl + API_ENDPOINTS.AUTH.GET_LOGIN_USER_BY_ID}/${id}`)
  }

  loginUserProfileUpdate(userDetail: UserDetail) {
    return this.http.post(`${this.apiUrl + API_ENDPOINTS.AUTH.UPDATE_USER_PROFILE}`, userDetail)
  }

  getUserProfileDetailById(userId: any) {
    return this.http.get<UserDetail[]>(`${this.apiUrl + API_ENDPOINTS.AUTH.GET_USER_PROFILE}/${userId}`)
  }

  // In client.service.ts
// client.service.ts
// getImageUrl(path: string): string {
//   if (!path) return ''; // Return empty string if path is null or undefined
//   // Normalize path: remove leading slashes and replace incorrect folder names
//   let cleanPath = path.replace(/^\/+/, ''); // Remove leading slashes
//   cleanPath = cleanPath.replace(/^UploadedImage\/Mission\//, 'UploadedFiles/MissionImages/'); // Fix legacy path
//   return `${this.imageUrl}/${cleanPath}`;
// }


 // e.g., http://localhost:56577

 
 getImageUrl(relativePath: string): string {
    if (!relativePath) return '';
    // Ensure the path is correctly formatted
    const normalizedPath = relativePath.startsWith('/')
      ? relativePath.substring(1)
      : relativePath;
    return `${this.imageUrl}/${normalizedPath}`; // Use imageUrl instead of apiUrl
  }


}
