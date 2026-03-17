import { Component, inject, OnInit, signal, DestroyRef } from '@angular/core';
import { DecimalPipe } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ProgressApiService, WeightPoint, MeasurementDto, PhotoDto } from '../../../core/progress-api.service';

@Component({
  selector: 'app-progress',
  imports: [DecimalPipe, FormsModule],
  template: `
    <div class="p-8">
      <div class="flex items-center justify-between mb-8">
        <div>
          <h1 class="text-3xl font-extrabold text-slate-900">Progress</h1>
          <p class="text-slate-500 mt-1">Track your body, see your transformation.</p>
        </div>
        <div class="flex gap-3">
          <button (click)="showLogWeight.set(!showLogWeight())"
                  class="px-5 py-2.5 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700 transition-colors flex items-center gap-2">
            <i class="pi pi-plus"></i> Log Weight
          </button>
        </div>
      </div>

      @if (showLogWeight()) {
        <div class="bg-white rounded-xl border border-slate-200 p-6 mb-6">
          <div class="flex items-end gap-4">
            <div class="flex-1">
              <label class="block text-sm font-medium text-slate-700 mb-1">Weight (kg)</label>
              <input type="number" [(ngModel)]="weightInput" step="0.1"
                     class="w-full px-4 py-3 rounded-xl border border-slate-300 focus:border-indigo-500 outline-none">
            </div>
            <button (click)="onLogWeight()" class="px-6 py-3 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700">
              Log
            </button>
          </div>
        </div>
      }

      @if (weightHistory().length > 0) {
        <div class="grid lg:grid-cols-3 gap-4 mb-8">
          <div class="bg-white rounded-xl border border-slate-200 p-5">
            <div class="text-sm font-medium text-slate-500 mb-1">Current Weight</div>
            <div class="text-2xl font-bold text-slate-900">{{latestWeight() | number:'1.1-1'}} kg</div>
          </div>
          <div class="bg-white rounded-xl border border-slate-200 p-5">
            <div class="text-sm font-medium text-slate-500 mb-1">Trend Weight</div>
            <div class="text-2xl font-bold text-slate-900">{{latestTrend() | number:'1.1-1'}} kg</div>
          </div>
          <div class="bg-white rounded-xl border border-slate-200 p-5">
            <div class="text-sm font-medium text-slate-500 mb-1">Entries</div>
            <div class="text-2xl font-bold text-slate-900">{{weightHistory().length}}</div>
          </div>
        </div>
      }

      <div class="grid lg:grid-cols-2 gap-6 mb-6">
        <div class="bg-white rounded-xl border border-slate-200 p-6">
          <h2 class="text-lg font-bold text-slate-900 mb-4">Weight Trend</h2>
          @if (weightHistory().length > 0) {
            <div class="h-48 flex items-end justify-between gap-1 px-2">
              @for (point of chartBars(); track $index) {
                <div class="flex-1 flex flex-col items-center gap-1">
                  <div class="w-full rounded-t bg-indigo-500 transition-all" [style.height.px]="point.height"></div>
                  <span class="text-xs text-slate-400 truncate">{{point.label}}</span>
                </div>
              }
            </div>
          } @else {
            <div class="text-center py-12 text-slate-400 text-sm">Log your first weight entry to see the chart.</div>
          }
        </div>
        <div class="bg-white rounded-xl border border-slate-200 p-6">
          <h2 class="text-lg font-bold text-slate-900 mb-4">Progress Photos</h2>
          @if (photos().length > 0) {
            <div class="grid grid-cols-3 gap-3">
              @for (photo of photos(); track photo.id) {
                <div class="aspect-[3/4] bg-slate-100 rounded-xl flex flex-col items-center justify-center border border-slate-200">
                  <i class="pi pi-image text-2xl text-slate-300 mb-2"></i>
                  <span class="text-xs text-slate-400">{{photo.date}}</span>
                  <span class="text-xs text-slate-300">{{photo.poseType}}</span>
                </div>
              }
            </div>
          } @else {
            <div class="text-center py-12 text-slate-400 text-sm">No photos yet.</div>
          }
        </div>
      </div>

      <div class="bg-white rounded-xl border border-slate-200 p-6">
        <div class="flex items-center justify-between mb-4">
          <h2 class="text-lg font-bold text-slate-900">Body Measurements</h2>
          <button (click)="showLogMeasurement.set(!showLogMeasurement())"
                  class="text-sm font-medium text-indigo-600 hover:text-indigo-700">
            + Log Measurement
          </button>
        </div>
        @if (showLogMeasurement()) {
          <div class="grid grid-cols-2 md:grid-cols-4 gap-3 mb-4">
            <div>
              <label class="block text-xs text-slate-500 mb-1">Waist (cm)</label>
              <input type="number" [(ngModel)]="mWaist" class="w-full px-3 py-2 rounded-lg border border-slate-300 text-sm">
            </div>
            <div>
              <label class="block text-xs text-slate-500 mb-1">Chest (cm)</label>
              <input type="number" [(ngModel)]="mChest" class="w-full px-3 py-2 rounded-lg border border-slate-300 text-sm">
            </div>
            <div>
              <label class="block text-xs text-slate-500 mb-1">Biceps L (cm)</label>
              <input type="number" [(ngModel)]="mBicepsL" class="w-full px-3 py-2 rounded-lg border border-slate-300 text-sm">
            </div>
            <div>
              <label class="block text-xs text-slate-500 mb-1">Thigh L (cm)</label>
              <input type="number" [(ngModel)]="mThighL" class="w-full px-3 py-2 rounded-lg border border-slate-300 text-sm">
            </div>
          </div>
          <button (click)="onLogMeasurement()" class="px-6 py-2 bg-indigo-600 text-white font-semibold rounded-xl hover:bg-indigo-700 text-sm mb-4">
            Save Measurement
          </button>
        }
        @if (measurements().length > 0) {
          <div class="grid grid-cols-2 md:grid-cols-4 gap-4">
            @for (m of latestMeasurements(); track m.label) {
              <div class="p-4 rounded-xl bg-slate-50 border border-slate-100">
                <div class="text-xs text-slate-400 mb-1">{{m.label}}</div>
                <div class="text-lg font-bold text-slate-900">{{m.value}}</div>
              </div>
            }
          </div>
        } @else {
          <div class="text-center py-6 text-slate-400 text-sm">No measurements logged yet.</div>
        }
      </div>
    </div>
  `,
})
export class ProgressPage implements OnInit {
  private readonly api = inject(ProgressApiService);
  private readonly destroyRef = inject(DestroyRef);

  readonly weightHistory = signal<WeightPoint[]>([]);
  readonly photos = signal<PhotoDto[]>([]);
  readonly measurements = signal<MeasurementDto[]>([]);
  readonly showLogWeight = signal(false);
  readonly showLogMeasurement = signal(false);

  weightInput = 0;
  mWaist = 0;
  mChest = 0;
  mBicepsL = 0;
  mThighL = 0;

  ngOnInit() {
    this.loadData();
  }

  loadData() {
    this.api.getWeightHistory().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (w) => this.weightHistory.set(w),
      error: () => this.weightHistory.set([]),
    });
    this.api.listPhotos().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (p) => this.photos.set(p),
      error: () => this.photos.set([]),
    });
    this.api.getMeasurements().pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: (m) => this.measurements.set(m),
      error: () => this.measurements.set([]),
    });
  }

  latestWeight(): number {
    const h = this.weightHistory();
    return h.length > 0 ? h[h.length - 1].weightKg : 0;
  }

  latestTrend(): number {
    const h = this.weightHistory();
    return h.length > 0 ? (h[h.length - 1].trendWeightKg ?? h[h.length - 1].weightKg) : 0;
  }

  chartBars(): { height: number; label: string }[] {
    const h = this.weightHistory();
    if (h.length === 0) return [];
    const last10 = h.slice(-10);
    const min = Math.min(...last10.map(p => p.weightKg));
    const max = Math.max(...last10.map(p => p.weightKg));
    const range = max - min || 1;
    return last10.map(p => ({
      height: 20 + ((p.weightKg - min) / range) * 130,
      label: p.date.substring(5),
    }));
  }

  latestMeasurements(): { label: string; value: string }[] {
    const m = this.measurements()[0];
    if (!m) return [];
    return [
      { label: 'Waist', value: m.waist ? `${m.waist} cm` : '-' },
      { label: 'Chest', value: m.chest ? `${m.chest} cm` : '-' },
      { label: 'Biceps (L)', value: m.bicepsL ? `${m.bicepsL} cm` : '-' },
      { label: 'Thigh (L)', value: m.thighL ? `${m.thighL} cm` : '-' },
    ];
  }

  onLogWeight() {
    if (this.weightInput <= 0) return;
    this.api.logWeight(this.weightInput).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: () => {
        this.showLogWeight.set(false);
        this.loadData();
      },
      error: () => {},
    });
  }

  onLogMeasurement() {
    this.api.logMeasurements({
      waist: this.mWaist || null,
      chest: this.mChest || null,
      bicepsL: this.mBicepsL || null,
      thighL: this.mThighL || null,
    }).pipe(
      takeUntilDestroyed(this.destroyRef)
    ).subscribe({
      next: () => {
        this.showLogMeasurement.set(false);
        this.loadData();
      },
      error: () => {},
    });
  }
}
